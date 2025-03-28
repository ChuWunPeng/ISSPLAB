
// TAGMFCDemoDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "TAGMFCDemo.h"
#include "TAGMFCDemoDlg.h"
#include "afxdialogex.h"
#include "json/json.h"
#include <time.h>
#include "IRCamera.h"

using namespace std;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#define WM_UPDATE_TAG	(WM_USER + 100)
// CTAGMFCDemoDlg 对话框


Json::Value tagValues;

CTAGMFCDemoDlg::CTAGMFCDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CTAGMFCDemoDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

	m_Camera = NULL;
	m_UserValue = _T("");
	m_PasswordValue = _T("");
}

void CTAGMFCDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_IPADDRESSValue, m_IPAddress);
	DDX_Text(pDX, IDC_EUser, m_UserValue);
	DDX_Text(pDX, IDC_EPassword, m_PasswordValue);
	DDX_Control(pDX, IDC_DataGrid, m_Grid);
}

BEGIN_MESSAGE_MAP(CTAGMFCDemoDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BConnect, &CTAGMFCDemoDlg::OnBnClickedBconnect)
	ON_MESSAGE(WM_UPDATE_TAG, &CTAGMFCDemoDlg::OnUpdateTag)
	ON_WM_CLOSE()
END_MESSAGE_MAP()


// CTAGMFCDemoDlg 消息处理程序

BOOL CTAGMFCDemoDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	InitializeCriticalSection(&m_TagBuf_cs);
	GridCtrlInit();
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CTAGMFCDemoDlg::OnPaint()
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

//当用户拖动最小化窗口时系统调用此函数取得光标
//显示。
HCURSOR CTAGMFCDemoDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CTAGMFCDemoDlg::GridCtrlInit()
{
	m_Grid.SetEditable(false);
	m_Grid.SetTextBkColor(RGB(0xFF, 0xFF, 0xE0));//黄色背景
	m_Grid.SetRowCount(8);
	m_Grid.SetColumnCount(4);
	m_Grid.SetFixedRowCount(1); //表头为一行
	m_Grid.SetItemText(0,0,_T("标记流名称"));
	m_Grid.SetItemText(0,1,_T("最高温"));
	m_Grid.SetItemText(0,2,_T("最低温"));
	m_Grid.SetItemText(0,3,_T("平均温"));
}

void CTAGMFCDemoDlg::OnBnClickedBconnect()
{
	// TODO: 在此添加控件通知处理程序代码
	if (m_Camera)
	{
		delete m_Camera;
		GetDlgItem(IDC_BConnect)->SetWindowText("连接");
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
			delete m_Camera;
		}

		try
		{
			m_Camera = new IRCamera((LPCSTR)ip,(LPCSTR)m_UserValue,(LPCSTR)m_PasswordValue);
		}
		catch(std::exception & e)
		{
			m_Camera = NULL;
			GetDlgItem(IDC_BConnect)->SetWindowText("连接");
			AfxMessageBox(e.what());
			return;
		}

		try
		{
			m_Camera->StartStream(CTAGMFCDemoDlg::Grabber,this);
			GetDlgItem(IDC_BConnect)->SetWindowText("断开");
		}
		catch(std::exception & e)
		{
			delete m_Camera;
			m_Camera = NULL;

			GetDlgItem(IDC_BConnect)->SetWindowText("连接");
			AfxMessageBox(e.what());
			return;
		}
	}
}

void CALLBACK CTAGMFCDemoDlg::Grabber(int error, streamsdk_st_buffer * buffer, void * user_data)
{
	CTAGMFCDemoDlg * dlg = (CTAGMFCDemoDlg *)user_data;

	if (error != STREAMSDK_EC_OK)
	{
		return;
	}

	std::size_t bufsz = buffer->buf_size;
	{
		CSLock lock(&dlg->m_TagBuf_cs);
		byte* tag_buffer = new byte[buffer->buf_size];
		byte* dPtr = (byte*)(buffer->buf_ptr);
		for (int i = 0; i < buffer->buf_size; i++)
		{
			tag_buffer[i] = dPtr[i];
		}
		std::string tagValueStr = (char*)tag_buffer;
		Json::Reader reader;
		Json::Value root;
		if(reader.parse((char*)tag_buffer,(char*)tag_buffer+bufsz, root))
		{
			tagValues = root;
		}
	}

	dlg->PostMessageA(WM_UPDATE_TAG);
}

LRESULT CTAGMFCDemoDlg::OnUpdateTag(WPARAM wParam, LPARAM lParam)
{
	UpdateTag();
	return 0;
}

void CTAGMFCDemoDlg::UpdateTag()	 
{
	m_Grid.SetItemText(1,0,_T("全局"));
	char maxValue[] = "";
	sprintf(maxValue, "%.1f ℃", tagValues["max"]["t"].asFloat());
	m_Grid.SetItemText(1,1,maxValue);
	char minValue[] = "";
	sprintf(minValue, "%.1f ℃", tagValues["min"]["t"].asFloat());
	m_Grid.SetItemText(1,2,minValue);
	int rowIndex = 2;
	for (auto iter= tagValues.begin(); iter != tagValues.end(); iter++)
	{
		string name = iter.name();
		if (name == "max" || name == "min")
		{
			continue;
		}
		else
		{
			m_Grid.SetItemText(rowIndex,0,name.c_str());
			if (tagValues[name].isMember("max"))
			{
				char sub_maxValue[10] = "";
				sprintf(sub_maxValue, "%.1f ℃", tagValues[name]["max"]["t"].asFloat());
				m_Grid.SetItemText(rowIndex,1,sub_maxValue);
				char sub_minValue[10] = "";
				sprintf(sub_minValue, "%.1f ℃", tagValues[name]["min"]["t"].asFloat());
				m_Grid.SetItemText(rowIndex,2,sub_minValue);
				m_Grid.SetItemText(rowIndex,3,_T("-"));
			}
			else
			{
				m_Grid.SetItemText(rowIndex,1,_T("-"));
				m_Grid.SetItemText(rowIndex,2,_T("-"));
				char value[10] = "";
				sprintf(value, "%.1f ℃", value);
				m_Grid.SetItemText(rowIndex,3,value);
			}
		}
		rowIndex++;
	}
	m_Grid.Refresh();
	UpdateData(FALSE);
}

void CTAGMFCDemoDlg::OnClose()
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值

	CDialogEx::OnClose();
	if (m_Camera)
	{
		delete m_Camera;
	}
}
