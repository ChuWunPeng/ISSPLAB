
// VIDEOMFCDemoDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "VIDEOMFCDemo.h"
#include "VIDEOMFCDemoDlg.h"
#include "afxdialogex.h"
#include "json/json.h"
#include <time.h>
#include "IRCamera.h"

using namespace std;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CVIDEOMFCDemoDlg 对话框

#define WM_UPDATE_TABLE	(WM_USER + 100)

uint16_t ** RawValues;
float ** TValues;


CVIDEOMFCDemoDlg::CVIDEOMFCDemoDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CVIDEOMFCDemoDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_Camera = NULL;
	m_UserValue = _T("");
	m_PasswordValue = _T("");
	m_RegionHeightValue = 0;
	m_RegionWidthValue = 0;
	m_RegionXValue = 1;
	m_RegionYValue = 1;
}

void CVIDEOMFCDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_IPADDRESSValue, m_IPAddress);
	DDX_Text(pDX, IDC_EUser, m_UserValue);
	DDX_Text(pDX, IDC_EPassword, m_PasswordValue);
	DDX_Control(pDX, IDC_DataGrid, m_Grid);
	DDX_Text(pDX, IDC_ERegionHeight, m_RegionHeightValue);
	DDV_MinMaxInt(pDX, m_RegionHeightValue, 1, 480);
	DDX_Text(pDX, IDC_ERegionWidth, m_RegionWidthValue);
	DDV_MinMaxInt(pDX, m_RegionWidthValue, 1, 640);
	DDX_Text(pDX, IDC_ERegionX, m_RegionXValue);
	DDV_MinMaxInt(pDX, m_RegionXValue, 0, 640);
	DDX_Text(pDX, IDC_ERegionY, m_RegionYValue);
	DDV_MinMaxInt(pDX, m_RegionYValue, 0, 480);
}

BEGIN_MESSAGE_MAP(CVIDEOMFCDemoDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BConnect, &CVIDEOMFCDemoDlg::OnBnClickedBconnect)
	ON_BN_CLICKED(IDC_BConnect2, &CVIDEOMFCDemoDlg::OnBnClickedBconnect2)
	ON_MESSAGE(WM_UPDATE_TABLE, &CVIDEOMFCDemoDlg::OnUpdateTable)
	ON_WM_CLOSE()
END_MESSAGE_MAP()


// CVIDEOMFCDemoDlg 消息处理程序

BOOL CVIDEOMFCDemoDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	((CButton *)GetDlgItem(IDC_RRaw))->SetCheck(TRUE);
	((CButton *)GetDlgItem(IDC_RTemper))->SetCheck(FALSE);
	m_RegionXValue = 0;
	m_RegionYValue = 0;
	m_RegionWidthValue = 1;
	m_RegionHeightValue = 1;
	UpdateData(FALSE);
	InitializeCriticalSection(&m_RawBuf_cs);
	GridCtrlInit();
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CVIDEOMFCDemoDlg::OnPaint()
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
HCURSOR CVIDEOMFCDemoDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CVIDEOMFCDemoDlg::GridCtrlInit()
{
	m_Grid.SetEditable(false);
	m_Grid.SetTextBkColor(RGB(0xFF, 0xFF, 0xE0));//黄色背景
	UpdateData(TRUE);
	m_Grid.SetRowCount(m_RegionHeightValue+1);
	m_Grid.SetColumnCount(m_RegionWidthValue+1);
	m_Grid.SetFixedRowCount(1);
	m_Grid.SetFixedColumnCount(1);
	m_Grid.SetColumnWidth(0, 50);
	for (int row = 1; row < m_Grid.GetRowCount(); row++)
	{
		char value[10] = "";
		sprintf(value, "%d", row+m_RegionYValue-1);
		m_Grid.SetItemText(row,0,value);
	}
	for (int col = 1; col < m_Grid.GetColumnCount(); col++)
	{
		m_Grid.SetColumnWidth(col, 50);
		char value[10] = "";
		sprintf(value, "%d", col+m_RegionXValue-1);
		m_Grid.SetItemText(0,col,value);

	}
	m_Grid.Refresh();
}


void CVIDEOMFCDemoDlg::OnBnClickedBconnect()
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
			m_Camera->UpdateFactoryLUT();
			int width = m_Camera->getWidth();
			int height = m_Camera->getHeight();
			RawValues = (uint16_t **)malloc(sizeof(uint16_t*)*width);
			TValues = (float **)malloc(sizeof(float*)*width);
			for(int i=0;i<width;i++)
			{
				RawValues[i]=(uint16_t*)malloc(sizeof(uint16_t)*height);
				TValues[i] = (float *)malloc(sizeof(float)*height);
			}
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
			m_Camera->StartStream(CVIDEOMFCDemoDlg::Grabber,this);
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

void CALLBACK CVIDEOMFCDemoDlg::Grabber(int error, streamsdk_st_buffer * buffer, void * user_data)
{
	CVIDEOMFCDemoDlg * dlg = (CVIDEOMFCDemoDlg *)user_data;

	if (error != STREAMSDK_EC_OK)
	{
		return;
	}

	{
		CSLock lock(&dlg->m_RawBuf_cs);
		int width = dlg->m_Camera->getWidth();
		int height = dlg->m_Camera->getHeight();
		int i=0;
		int j=0;
		uint16_t * values = (uint16_t *) buffer->buf_ptr;
		for (int m=0;m<width*height;m++)
		{
			j=m/width;
			i=m%width;
			RawValues[i][j]=values[m];
			if (RawValues[i][j]<dlg->m_Camera->From)
			{
				TValues[i][j] = dlg->m_Camera->LUT[dlg->m_Camera->From];
			}
			else if (RawValues[i][j]>dlg->m_Camera->To)
			{
				TValues[i][j] = dlg->m_Camera->LUT[dlg->m_Camera->To];
			}
			else
			{
				TValues[i][j] = dlg->m_Camera->LUT[RawValues[i][j]];
			}
		}
	}
	/** 注意: 如果在回调函数中使用SendMessage 可能会导致死锁 **/
	dlg->PostMessageA(WM_UPDATE_TABLE);
}

LRESULT CVIDEOMFCDemoDlg::OnUpdateTable(WPARAM wParam, LPARAM lParam)
{
	int rawCheck = ((CButton *)GetDlgItem(IDC_RRaw))->GetCheck();
	if (rawCheck == 1)
	{
		for (int i=1;i<m_RegionWidthValue+1;i++)
		{
			for (int j=1;j<m_RegionHeightValue+1;j++)
			{
				char value[10] = "";
				sprintf(value, "%d", RawValues[i-1][j-1]);
				m_Grid.SetItemText(j,i,value);
			}
		}
	}
	else
	{
		for (int i=1;i<m_RegionWidthValue+1;i++)
		{
			for (int j=1;j<m_RegionHeightValue+1;j++)
			{
				char value[10] = "";
				sprintf(value, "%.1f", TValues[i-1][j-1]);
				m_Grid.SetItemText(j,i,value);
			}
		}
	}
	m_Grid.Refresh();
	UpdateData(FALSE);
	return 0;
}


void CVIDEOMFCDemoDlg::OnBnClickedBconnect2()
{
	// TODO: 在此添加控件通知处理程序代码
	GridCtrlInit();
}

void CVIDEOMFCDemoDlg::OnClose()
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	CDialogEx::OnClose();

	if (m_Camera)
	{
		delete m_Camera;
	}
}