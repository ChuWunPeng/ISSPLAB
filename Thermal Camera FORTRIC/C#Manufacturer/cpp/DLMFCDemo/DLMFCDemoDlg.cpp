
// DLMFCDemoDlg.cpp : 实现文件
//
#include "stdafx.h"
#include "DLMFCDemo.h"
#include "DLMFCDemoDlg.h"
#include "afxdialogex.h"
#include "json/json.h"
#include <time.h>
#include "palettes.h"
#include "yuv2rgb.h"
#include "IRCamera.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define WM_UPDATE_TEMPERATURE	(WM_USER + 100)
#define WM_UPDATE_IMAGE			(WM_USER + 101)

// 用于应用程序“关于”菜单项的 CAboutDlg 对话框

class CAboutDlg : public CDialogEx
{
public:
	CAboutDlg();

// 对话框数据
	enum { IDD = IDD_ABOUTBOX };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

// 实现
protected:
	DECLARE_MESSAGE_MAP()
public:

};

CAboutDlg::CAboutDlg() : CDialogEx(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}
BEGIN_MESSAGE_MAP(CAboutDlg, CDialogEx)
END_MESSAGE_MAP()


SimpleImageBuffer::SimpleImageBuffer()
{
	m_ImageBuf = NULL;
	m_ImageBufSize = 0;
}

SimpleImageBuffer::~SimpleImageBuffer()
{
	delete m_ImageBuf;
}

void SimpleImageBuffer::resize(std::size_t sz)
{
	/** 只在缓冲区过小时重新分配 **/
	if(m_ImageBufSize < sz)
	{
		delete m_ImageBuf;
		m_ImageBuf = new BYTE[sz];
		m_ImageBufSize = sz;
	}

	m_Size = sz;
}

void SimpleImageBuffer::copyImage(void * ptr,std::size_t sz)
{
	resize(sz);
	memcpy(m_ImageBuf,ptr,sz);
}

BYTE * SimpleImageBuffer::getImage()
{
	return m_ImageBuf;
}

// CDLMFCDemoDlg 对话框
CDLMFCDemoDlg::CDLMFCDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CDLMFCDemoDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	m_CurrentView = H264View;
	m_Camera = NULL;
	m_TempAcqThread = NULL;
	m_StreamType = IRCamera::PrimaryStream;
	m_RawPalette = Iron;
}

void CDLMFCDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_PE_Image, m_Image);
	DDX_Control(pDX, IDC_IPADDRESSValue, m_IPAddress);
	DDX_Text(pDX, IDC_EPassword, m_PasswordValue);
	DDX_Text(pDX, IDC_EUser, m_UserValue);
	DDX_Control(pDX, IDC_CBPalette, m_Palettes);
	//DDX_Control(pDX, IDC_STempValue, m_TempValue);
}

BEGIN_MESSAGE_MAP(CDLMFCDemoDlg, CDialogEx)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BConnect, &CDLMFCDemoDlg::OnBnClickedBconnect)
	ON_BN_CLICKED(IDC_BAutoFocus, &CDLMFCDemoDlg::OnBnClickedBautofocus)
	ON_BN_CLICKED(IDC_RH264, &CDLMFCDemoDlg::OnBnClickedRh264)
	ON_BN_CLICKED(IDC_RRaw, &CDLMFCDemoDlg::OnBnClickedRraw)
	ON_BN_CLICKED(IDC_RPri, &CDLMFCDemoDlg::OnBnClickedRpri)
	ON_BN_CLICKED(IDC_RSub, &CDLMFCDemoDlg::OnBnClickedRsub)
	ON_CBN_SELCHANGE(IDC_CBPalette, &CDLMFCDemoDlg::OnCbnSelchangeCbpalette)
	ON_WM_TIMER()
	ON_WM_SHOWWINDOW()
	ON_WM_MOUSEMOVE()
	ON_WM_CLOSE()
	ON_MESSAGE(WM_UPDATE_TEMPERATURE, &CDLMFCDemoDlg::OnUpdateTemperature)
	ON_MESSAGE(WM_UPDATE_IMAGE, &CDLMFCDemoDlg::OnUpdateImage)
	ON_BN_CLICKED(IDC_BRawSnap, &CDLMFCDemoDlg::OnClickedBrawsnap)
	ON_BN_CLICKED(IDC_BH264Snap, &CDLMFCDemoDlg::OnClickedBh264snap)
END_MESSAGE_MAP()




// CDLMFCDemoDlg 消息处理程序

BOOL CDLMFCDemoDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 将“关于...”菜单项添加到系统菜单中。

	// IDM_ABOUTBOX 必须在系统命令范围内。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		BOOL bNameValid;
		CString strAboutMenu;
		bNameValid = strAboutMenu.LoadString(IDS_ABOUTBOX);
		ASSERT(bNameValid);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码

	CButton* stream=(CButton*)GetDlgItem(IDC_RH264);
	stream->SetCheck(1);
	m_CurrentView = H264View;

	((CButton *)GetDlgItem(IDC_RPri))->SetCheck(TRUE);
	m_StreamType = IRCamera::PrimaryStream;

	UpdateData(FALSE);

	InitializeCriticalSection(&m_MousemovePoint_cs);
	InitializeCriticalSection(&m_ImageBuf_cs); 
	DisableView();
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CDLMFCDemoDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialogEx::OnSysCommand(nID, lParam);
	}
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CDLMFCDemoDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作区矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}


void CDLMFCDemoDlg::SwitchView(ViewType v)
{
	if (m_Camera)
	{
		GetDlgItem(IDC_IPADDRESSValue)->EnableWindow(false);
		GetDlgItem(IDC_EUser)->EnableWindow(false);
		GetDlgItem(IDC_EPassword)->EnableWindow(false);
		GetDlgItem(IDC_RH264)->EnableWindow(false);
		GetDlgItem(IDC_RRaw)->EnableWindow(false);
		GetDlgItem(IDC_BConnect)->SetWindowText("断开");
		
		CString temp;
		temp.Format("热像仪已连接:%s",m_Camera->getIP().c_str());
		GetDlgItem(IDC_SCameraStatus)->SetWindowText(temp);

		if (v == H264View)
		{
			GetDlgItem(IDC_RPri)->EnableWindow(true);
			GetDlgItem(IDC_RSub)->EnableWindow(true);

			std::vector<std::string> plt_list;
			m_Camera->GetPaletteList(plt_list);

			std::vector<std::string>::iterator iter = plt_list.begin();
			for( ;iter != plt_list.end();iter++)
			{
				m_Palettes.AddString(iter->c_str());
			}

			m_Palettes.SelectString(0,m_Camera->GetCurrentPalette().c_str());
		}
		else
		{
			GetDlgItem(IDC_RPri)->EnableWindow(false);
			GetDlgItem(IDC_RSub)->EnableWindow(false);

			m_Palettes.ResetContent();
			m_Palettes.AddString("灰度");
			m_Palettes.AddString("铁红");
			m_Palettes.AddString("彩虹");
			m_Palettes.SetCurSel(2);

			m_RawPalette = Iron;
		}

		
		GetDlgItem(IDC_CBPalette)->EnableWindow(true);
		GetDlgItem(IDC_BRawSnap)->EnableWindow(true);
		GetDlgItem(IDC_BH264Snap)->EnableWindow(true);
		GetDlgItem(IDC_BAutoFocus)->EnableWindow(true);

		
		CRect rect;
		GetDlgItem(IDC_PE_Image)->GetClientRect(rect);
		int height = rect.bottom - rect.top;
		int width = rect.right - rect.left;
		m_wRatio = (float)width/m_Camera->getWidth();
		m_hRatio = (float)height/m_Camera->getHeight();
	}
	else
	{
		DisableView();
	}
}

void CDLMFCDemoDlg::DisableView()
{
	GetDlgItem(IDC_IPADDRESSValue)->EnableWindow(true);
	GetDlgItem(IDC_EUser)->EnableWindow(true);
	GetDlgItem(IDC_EPassword)->EnableWindow(true);
	GetDlgItem(IDC_RH264)->EnableWindow(true);
	GetDlgItem(IDC_RRaw)->EnableWindow(true);
	GetDlgItem(IDC_BConnect)->SetWindowText("连接");
	GetDlgItem(IDC_SCameraStatus)->SetWindowText("热像仪未连接");
	GetDlgItem(IDC_RPri)->EnableWindow(false);
	GetDlgItem(IDC_RSub)->EnableWindow(false);
	GetDlgItem(IDC_CBPalette)->EnableWindow(false);
	GetDlgItem(IDC_BRawSnap)->EnableWindow(false);
	GetDlgItem(IDC_BH264Snap)->EnableWindow(false);
	GetDlgItem(IDC_BAutoFocus)->EnableWindow(false);

}

BOOL CDLMFCDemoDlg::SwitchH264Stream(IRCamera::stream_type st)
{
	if(m_Camera)
	{
		try
		{
			m_Camera->StopStream();
			m_Camera->StartH264Stream(st,&CDLMFCDemoDlg::GrabberH264,this);
		}
		catch(...)
		{
			return FALSE;
		}
	}
	return TRUE;
}


void CALLBACK CDLMFCDemoDlg::GrabberRaw(int error, streamsdk_st_buffer * buffer, void * user_data)
{
	CDLMFCDemoDlg * dlg = (CDLMFCDemoDlg *)user_data;

	if (error != STREAMSDK_EC_OK)
	{
		return;
	}

	std::size_t bufsz = dlg->m_Camera->getWidth() * dlg->m_Camera->getHeight() * 3;

	{
		/** 注意，这里将全辐射图像转换放在了回调函数中，
		    回调函数中的处理时间不能过长，否则会导致丢帧 **/
		CSLock lock(&dlg->m_ImageBuf_cs);
	
		dlg->m_ImageBuf.m_Width = dlg->m_Camera->getWidth();
		dlg->m_ImageBuf.m_Height = dlg->m_Camera->getHeight();
		dlg->m_ImageBuf.resize(bufsz);

		convertRawToBGR((RawValue *)buffer->buf_ptr,dlg->m_Camera->getWidth(),dlg->m_Camera->getHeight()
			,dlg->m_ImageBuf.getImage(),dlg->m_RawPalette);
	}
	/** 注意: 如果在回调函数中使用SendMessage 可能会导致死锁 **/
	dlg->PostMessageA(WM_UPDATE_IMAGE);
}


void CALLBACK CDLMFCDemoDlg::GrabberH264(int error,streamsdk_st_image *img,void * user_data)
{
	CDLMFCDemoDlg * dlg = (CDLMFCDemoDlg *)user_data;
	if (error != STREAMSDK_EC_OK)
		return;
	
	std::size_t bufsz = img->img_h * img->img_linesize;

	{
		CSLock lock(&dlg->m_ImageBuf_cs);
	
		dlg->m_ImageBuf.m_Width = img->img_w;
		dlg->m_ImageBuf.m_Height = img->img_h;
		dlg->m_ImageBuf.copyImage(img->img_ptr,bufsz);
	}
	/** 注意: 如果在回调函数中使用SendMessage 可能会导致死锁 **/
	//dlg->SendMessage(WM_UPDATE_IMAGE);
	dlg->PostMessageA(WM_UPDATE_IMAGE);
}

UINT CDLMFCDemoDlg::_acquireTemperature(LPVOID pthread)
{
	CDLMFCDemoDlg *dlg = (CDLMFCDemoDlg*) pthread;
	while(!dlg->m_TempAcqClose)
	{
		/** 过快访问设备可能会导致指令溢出 **/
		Sleep(100);
	
		float temp = -273.15f;
		try
		{
			CPoint pt;

			{
				CSLock lock(&dlg->m_MousemovePoint_cs);
				pt = dlg->m_MousemovePoint;
			}

			/** 只传递一个float变量，所以没有使用互斥量 **/
			dlg->m_TempAcq = dlg->m_Camera->GetTemperature(pt.x,pt.y);
			for (int i = 0;i<384;i++)
			{
				for(int j = 0;j<288;j++)
				{
					dlg->m_TempAcq = dlg->m_Camera->GetTemperature(i,j);
				}
			}
			dlg->PostMessage(WM_UPDATE_TEMPERATURE);
		}
		catch(std::exception & )
		{
		}
	}
	
	return 0;
}

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CDLMFCDemoDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CDLMFCDemoDlg::OnBnClickedBconnect()
{
	if (m_Camera)
	{
		if(m_TempAcqThread)
		{
			m_TempAcqClose = TRUE;
			WaitForSingleObject(m_TempAcqThread->m_hThread,INFINITE);
			/** 线程资源会在线程退出时自动回收 **/
			m_TempAcqThread = NULL;
		}

		delete m_Camera;
		DisableView();
		m_Camera = NULL;

	}
	else
	{
		UpdateData(TRUE);
		if(m_IPAddress.IsBlank())
			return;

		byte byte0,byte1,byte2,byte3;
		m_IPAddress.GetAddress(byte0,byte1,byte2,byte3);

		CString ip;
		ip.Format("%d.%d.%d.%d",byte0,byte1,byte2,byte3);

		if(m_Camera)
		{
			m_TempAcqClose = TRUE;
			WaitForSingleObject(m_TempAcqThread->m_hThread,INFINITE);
			m_TempAcqThread = NULL;
			delete m_Camera;
		}
		
		try
		{
			m_Camera = new IRCamera((LPCSTR)ip,(LPCSTR)m_UserValue,(LPCSTR)m_PasswordValue);
		}
		catch(std::exception & e)
		{
			m_Camera = NULL;
			DisableView();
			AfxMessageBox(e.what());
			return;
		}

		try
		{
			SwitchView(m_CurrentView);
		
			if (m_CurrentView == H264View)
			{
				m_Camera->StartH264Stream(m_StreamType,&CDLMFCDemoDlg::GrabberH264,this);
			} 
			else
			{
				m_Camera->StartRawStream(CDLMFCDemoDlg::GrabberRaw,this);
			}
		}
		catch(std::exception & e)
		{
			delete m_Camera;
			m_Camera = NULL;
			
			DisableView();
			AfxMessageBox(e.what());
			return;
		}

		m_TempAcqClose = FALSE;
		m_TempAcqThread = AfxBeginThread(&CDLMFCDemoDlg::_acquireTemperature, (LPVOID)this , THREAD_PRIORITY_NORMAL , 0 , 0 , NULL);
	}
}

LRESULT CDLMFCDemoDlg::OnUpdateTemperature(WPARAM wParam, LPARAM lParam)  
{  
	CString tstr;
	tstr.Format("%.1f ℃",m_TempAcq);

	GetDlgItem(IDC_STempValue)->SetWindowText(tstr);

	return 0;  
}  

LRESULT CDLMFCDemoDlg::OnUpdateImage(WPARAM wParam, LPARAM lParam)
{
	ShowImage();
	return 0;
}

void CDLMFCDemoDlg::OnClickedBrawsnap()
{
	// TODO: 在此添加控件通知处理程序代码
	if (m_defaultSavePath == "")
	{
		TCHAR           szFolderPath[MAX_PATH] = {0};  
		m_defaultSavePath = TEXT("");  

		BROWSEINFO      sInfo;  
		::ZeroMemory(&sInfo, sizeof(BROWSEINFO));  
		sInfo.pidlRoot   = 0;  
		sInfo.lpszTitle   = _T("请选择默认图片保存路径");  
		sInfo.ulFlags   = BIF_RETURNONLYFSDIRS|BIF_EDITBOX|BIF_DONTGOBELOWDOMAIN;  
		sInfo.lpfn     = NULL;  

		// 显示文件夹选择对话框  
		LPITEMIDLIST lpidlBrowse = ::SHBrowseForFolder(&sInfo);   
		if (lpidlBrowse != NULL)  
		{  
			// 取得文件夹名  
			if (::SHGetPathFromIDList(lpidlBrowse,szFolderPath))    
			{  
				m_defaultSavePath = szFolderPath;  
			}  
		}  
		if(lpidlBrowse != NULL)  
		{  
			::CoTaskMemFree(lpidlBrowse);  
		}
	}  
	SnapRaw(m_Camera->SnapRaw());
}


void CDLMFCDemoDlg::OnClickedBh264snap()
{
	// TODO: 在此添加控件通知处理程序代码
	if (m_defaultSavePath == "")
	{
		TCHAR           szFolderPath[MAX_PATH] = {0};  
		m_defaultSavePath = TEXT("");  

		BROWSEINFO      sInfo;  
		::ZeroMemory(&sInfo, sizeof(BROWSEINFO));  
		sInfo.pidlRoot   = 0;  
		sInfo.lpszTitle   = _T("请选择默认图片保存路径");  
		sInfo.ulFlags   = BIF_RETURNONLYFSDIRS|BIF_EDITBOX|BIF_DONTGOBELOWDOMAIN;  
		sInfo.lpfn     = NULL;  

		// 显示文件夹选择对话框  
		LPITEMIDLIST lpidlBrowse = ::SHBrowseForFolder(&sInfo);   
		if (lpidlBrowse != NULL)  
		{  
			// 取得文件夹名  
			if (::SHGetPathFromIDList(lpidlBrowse,szFolderPath))    
			{  
				m_defaultSavePath = szFolderPath;  
			}  
		}  
		if(lpidlBrowse != NULL)  
		{  
			::CoTaskMemFree(lpidlBrowse);  
		}
	} 
	SnapH264(m_Camera->SnapH264());
}

void CDLMFCDemoDlg::SnapRaw(const char* buf)
{
	int nWidth = m_Camera->getWidth();
	int nHeight = m_Camera->getHeight();

	BITMAPINFO bi; 
	ZeroMemory(&bi, sizeof(BITMAPINFO));
	bi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	bi.bmiHeader.biWidth = nWidth;
	bi.bmiHeader.biHeight = -nHeight;
	bi.bmiHeader.biPlanes = 1;
	bi.bmiHeader.biBitCount = 24;

	long nLnBytes = (bi.bmiHeader.biWidth*3+3)/4*4;  
	int count = nLnBytes*abs(bi.bmiHeader.biHeight);
	BYTE *pData = new BYTE[count];   
	unsigned short ad_min = 65535,ad_max = 0;

	unsigned short* sPtr = (unsigned short*)(buf+16);
	for(int i = 0;i<nWidth*nHeight;i++)
	{
		unsigned short ADValue = sPtr[i];
		if (ADValue<ad_min)
		{
			ad_min = ADValue;
		}
		if(ADValue>ad_max)
		{
			ad_max = ADValue;
		}
	}
	int span = ad_max - ad_min;
	if(span<1)
	{
		span = 1;
	}

	for(int i=0; i<nHeight; i++)  
	{  
		for(int j = 0;j<nWidth;j++)
		{
			int diff = sPtr[j + i * nWidth] - ad_min;
			if (diff<0)
			{
				diff = 0;
			}
			else if (diff>span)
			{
				diff = span;
			}
			int index = diff*255/span;
			if (index<0)
			{
				index = 0;
			}
			else if (index>255)
			{
				index = 255;
			}
			pData[i*nLnBytes+j*3+0] = Grey[index][0];
			pData[i*nLnBytes+j*3+1] = Grey[index][1];
			pData[i*nLnBytes+j*3+2] = Grey[index][2];
		}
	}

	BITMAPFILEHEADER bh;
	ZeroMemory(&bh, sizeof(BITMAPFILEHEADER));
	bh.bfType = 0x4d42; //bitmap 
	bh.bfOffBits = sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER);
	bh.bfSize = bh.bfOffBits + ((nWidth*nHeight)*3);

	CFile file;
	CString fileHader,filePostfix;
	fileHader = "IR_";
	filePostfix = ".bmp";
	SYSTEMTIME st = {0};
	GetLocalTime(&st);
	char timeInfo[14];
	sprintf(timeInfo,"d%02d%02d%02d%02d%02d",
		st.wYear,
		st.wMonth,
		st.wDay,
		st.wHour,
		st.wMinute,
		st.wSecond);
	CString szFileName = m_defaultSavePath+"/"+fileHader+timeInfo+filePostfix;
	if(file.Open(szFileName, CFile::modeCreate | CFile::modeWrite))
	{ 
		file.Write(&bh, sizeof(BITMAPFILEHEADER));
		file.Write(&(bi.bmiHeader), sizeof(BITMAPINFOHEADER));
		file.Write(pData, 3 * nWidth * nHeight);
		file.Close();
		CString messageHeader = "快照保存位置：";
		CString message = messageHeader+szFileName;
		MessageBox(message, NULL, MB_OK);
	}
	free(pData);
}


void CDLMFCDemoDlg::SnapH264(const char* buf)
{
	int nWidth = m_Camera->getWidth();
	int nHeight = m_Camera->getHeight();

	BITMAPINFO bi; 
	ZeroMemory(&bi, sizeof(BITMAPINFO));
	bi.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
	bi.bmiHeader.biWidth = nWidth;
	bi.bmiHeader.biHeight = -nHeight;
	bi.bmiHeader.biPlanes = 1;
	bi.bmiHeader.biBitCount = 24;

	unsigned char* rgb = new unsigned char[nWidth*nHeight*3];
	nv12_to_bgr(rgb,(const unsigned char*)(buf+16),nWidth,nHeight);
	
	BITMAPFILEHEADER bh;
	ZeroMemory(&bh, sizeof(BITMAPFILEHEADER));
	bh.bfType = 0x4d42; //bitmap 
	bh.bfOffBits = sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER);
	bh.bfSize = bh.bfOffBits + ((nWidth*nHeight)*3);

	CFile file;
	CString fileHader,filePostfix;
	fileHader = "IR_";
	filePostfix = ".bmp";
	SYSTEMTIME st = {0};
	GetLocalTime(&st);
	char timeInfo[14];
	sprintf(timeInfo,"d%02d%02d%02d%02d%02d",
		st.wYear,
		st.wMonth,
		st.wDay,
		st.wHour,
		st.wMinute,
		st.wSecond);
	CString szFileName = m_defaultSavePath+"/"+fileHader+timeInfo+filePostfix;
	if(file.Open(szFileName, CFile::modeCreate | CFile::modeWrite))
	{ 
		file.Write(&bh, sizeof(BITMAPFILEHEADER));
		file.Write(&(bi.bmiHeader), sizeof(BITMAPINFOHEADER));
		file.Write(rgb, 3 * nWidth * nHeight);
		file.Close();
		CString messageHeader = "快照保存位置：";
		CString message = messageHeader+szFileName;
		MessageBox(message, NULL, MB_OK);
	}
}


void CDLMFCDemoDlg::OnBnClickedBautofocus()
{
	if(m_Camera)
	{
		try
		{
			m_Camera->AutoFocus();
		}
		catch(...)
		{
		}
	}
}


void CDLMFCDemoDlg::OnBnClickedRh264()
{
	m_CurrentView = H264View;
	m_StreamType = IRCamera::PrimaryStream;
	((CButton *)GetDlgItem(IDC_RPri))->SetCheck(TRUE);
	((CButton *)GetDlgItem(IDC_RSub))->SetCheck(FALSE);
}

void CDLMFCDemoDlg::OnBnClickedRraw()
{
	m_CurrentView = RawView;
}

void CDLMFCDemoDlg::OnBnClickedRpri()
{
	m_StreamType = IRCamera::PrimaryStream;
	SwitchH264Stream(m_StreamType);
}

void CDLMFCDemoDlg::OnBnClickedRsub()
{
	m_StreamType = IRCamera::SubsidiaryStream;

	SwitchH264Stream(m_StreamType);
}


void CDLMFCDemoDlg::OnCbnSelchangeCbpalette()
{
	CString pltName;
	int index = m_Palettes.GetCurSel();
	m_Palettes.GetLBText(index,pltName);

	if (m_CurrentView == H264View)
	{
		m_Camera->SetCurrentPalette(pltName.GetBuffer(0));
	}
	else if(m_CurrentView == RawView)
	{
		if(pltName == "灰度")
		{
			m_RawPalette = Grey;
		}
		else if(pltName == "铁红")
		{
			m_RawPalette = Iron;
		}
		else if(pltName == "彩虹")
		{
			m_RawPalette = Rainbow;
		}
	}
}

void CDLMFCDemoDlg::ShowImage()
{	
	CDC * cdc = m_Image.GetDC();
	if (cdc == NULL)
		return;
	
	cdc->SaveDC();

	HBITMAP dibmap;
	BITMAPINFOHEADER bmih ;

	bmih.biSize          = sizeof (BITMAPINFOHEADER) ;
	bmih.biPlanes        = 1 ;
	bmih.biBitCount      = 24 ;
	bmih.biCompression   = BI_RGB ;
	bmih.biSizeImage     = 0 ;
	bmih.biXPelsPerMeter = 0 ;
	bmih.biYPelsPerMeter = 0 ;
	bmih.biClrUsed       = 0 ;
	bmih.biClrImportant  = 0 ;

	{
		CSLock lock(&m_ImageBuf_cs);
		
		bmih.biWidth         = m_ImageBuf.m_Width;
		bmih.biHeight        = m_ImageBuf.m_Height;
		BYTE * cbm_bits;

		dibmap = CreateDIBSection(cdc->GetSafeHdc(),(BITMAPINFO *) &bmih,0,(void **)&cbm_bits,NULL,0);
		if(dibmap == NULL)
			return;
		memcpy(cbm_bits,m_ImageBuf.getImage(),m_ImageBuf.m_Size);
	}

	CDC mdc;
	mdc.CreateCompatibleDC(cdc);
	mdc.SelectObject(dibmap);

	RECT rc; 
	m_Image.GetClientRect(&rc);
	int w = rc.right - rc.left;
	int h = rc.bottom - rc.top;  

	cdc->SetStretchBltMode(STRETCH_HALFTONE);
	cdc->StretchBlt(0,h - 1,w,-h,&mdc,
		0,0,bmih.biWidth,bmih.biHeight,SRCCOPY);

	cdc->RestoreDC(-1);
	ReleaseDC(cdc);
	mdc.DeleteDC();
	::DeleteObject(dibmap);
	
}

void CDLMFCDemoDlg::OnTimer(UINT_PTR nIDEvent)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	CDialogEx::OnTimer(nIDEvent);
}

void CDLMFCDemoDlg::OnShowWindow(BOOL bShow, UINT nStatus)
{
	CDialogEx::OnShowWindow(bShow, nStatus);
}


void CDLMFCDemoDlg::OnMouseMove(UINT nFlags, CPoint point)
{
	if(!m_Camera)
		return;

	ClientToScreen(&point);							//将鼠标坐标转换成屏幕坐标

	CRect rect;										//定义一个矩形框，包含左上角和右下角可访问成员
	GetDlgItem(IDC_PE_Image)->GetClientRect(rect);	//获取Picture控件的位置信息，存入rect中
	int height = rect.bottom - rect.top;
	int width = rect.right - rect.left;
	GetDlgItem(IDC_PE_Image)->ClientToScreen(rect);	//转换成屏幕坐标

	{
		CSLock lock(&m_MousemovePoint_cs);
	
		m_MousemovePoint.x = -1;
		m_MousemovePoint.y = -1;

		if (rect.PtInRect(point))						//判断point是否在rect内部
		{
			m_MousemovePoint.x = (point.x-rect.left)/m_wRatio;
			m_MousemovePoint.y = (point.y-rect.top)/m_hRatio;
		}
	}	

	CString pointStr;
	pointStr.Format("(%d,%d)",m_MousemovePoint.x,m_MousemovePoint.y);
	GetDlgItem(IDC_SLocValue)->SetWindowText(pointStr);
}


void CDLMFCDemoDlg::OnClose()
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值

	CDialogEx::OnClose();
	if (m_Camera)
	{
		delete m_Camera;
	}
}
