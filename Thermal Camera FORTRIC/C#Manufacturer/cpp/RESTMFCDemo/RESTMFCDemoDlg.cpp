
// RESTMFCDemoDlg.cpp : implementation file
//

#include "stdafx.h"
#include "RESTMFCDemo.h"
#include "RESTMFCDemoDlg.h"
#include "afxdialogex.h"
#include <fstream>
#include "RestCmd.h"
using namespace std;

#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// CRESTMFCDemoDlg dialog
static unsigned short CMD_PORT = 10080;


CRESTMFCDemoDlg::CRESTMFCDemoDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_RESTMFCDEMO_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_UserValue = _T("");
	m_PasswordValue = _T("");
	m_UrlValue = _T("");
	m_RequestValue = _T("");
	m_ResponseValue = _T("");
}

void CRESTMFCDemoDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LCTemplate, m_Template);
	DDX_Control(pDX, IDC_CBMethod, m_method);
	DDX_Control(pDX, IDC_EUrl, m_url);
	DDX_Control(pDX, IDC_IPADDRESSValue, m_IPAddress);
	DDX_Text(pDX, IDC_EUser, m_UserValue);
	DDX_Text(pDX, IDC_EPassword, m_PasswordValue);
	DDX_Text(pDX, IDC_EUrl, m_UrlValue);
	DDX_Text(pDX, IDC_Request, m_RequestValue);
	DDX_Text(pDX, IDC_Response, m_ResponseValue);
	DDX_Control(pDX, IDC_Request, m_Request);
	DDX_Control(pDX, IDC_Response, m_Response);
	DDX_Control(pDX, IDC_PStatus, m_PStatus);
}

BEGIN_MESSAGE_MAP(CRESTMFCDemoDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BSend, &CRESTMFCDemoDlg::OnBnClickedBsend)
	ON_NOTIFY(HDN_ITEMDBLCLICK, 0, &CRESTMFCDemoDlg::OnItemdblclickLctemplate)
	ON_NOTIFY(NM_DBLCLK, IDC_LCTemplate, &CRESTMFCDemoDlg::OnDblclkLctemplate)
	ON_CBN_SELCHANGE(IDC_CBMethod, &CRESTMFCDemoDlg::OnCbnSelchangeCbmethod)
END_MESSAGE_MAP()


// CRESTMFCDemoDlg message handlers

BOOL CRESTMFCDemoDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	m_method.ResetContent();
	m_method.AddString("GET");
	m_method.AddString("PUT");
	m_method.AddString("POST");
	m_method.AddString("DELETE");
	RefreshTemplate();
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CRESTMFCDemoDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CRESTMFCDemoDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}


void CRESTMFCDemoDlg::OnBnClickedBsend()
{
	// TODO: Add your control notification handler code here
	CImage image;
	HBITMAP hBmp; 
	UpdateData(TRUE);
	if(m_IPAddress.IsBlank())
		return;
	byte byte0,byte1,byte2,byte3;
	m_IPAddress.GetAddress(byte0,byte1,byte2,byte3);

	CString ip;
	ip.Format("%d.%d.%d.%d",byte0,byte1,byte2,byte3);
	GetDlgItem(IDC_Response)->SetWindowText("");
	GetDlgItem(IDC_Status)->SetWindowText("");
	/** 设置权限 **/
	RestCmd::setAuthorization((LPCSTR)m_UserValue,(LPCSTR)m_PasswordValue);
	RestCmd restcmd((LPCSTR)ip,CMD_PORT);
	int statusCode = 0;
	char c[10];
	try
	{
		/** 获取方法 **/
		CString method;
		int index = m_method.GetCurSel();
		m_method.GetLBText(index,method);
		/** 获取url **/
		CString url;
		m_url.GetWindowText(url);
		/** 获取待发送的指令 **/
		CString requestValue;
		m_Request.GetWindowText(requestValue);
		m_Response.SetWindowText("");
		restcmd.setTimeout(3000);
		Json::Reader reader;
		Json::Value jvRequest;
		Json::Value jvResponse;
		/** 根据相应方法发送http请求，并获取返回值和状态码 **/
		/** 状态码为200说明发送请求成功 **/
		if (method == "GET")
		{
			restcmd.restGet(LPCSTR(url),jvResponse,statusCode);
			GetDlgItem(IDC_Response)->SetWindowText(jvResponse.toStyledString().c_str());
		} 
		else if (method == "PUT")
		{
			reader.parse((LPCSTR)requestValue,jvRequest);
			restcmd.restPut(LPCSTR(url),jvRequest,NULL, statusCode);
		}
		else if (method == "POST")
		{
			reader.parse((LPCSTR)requestValue,jvRequest);
			restcmd.restPost(LPCSTR(url),jvRequest,NULL, statusCode);
		}
		else if (method == "DELETE")
		{
			restcmd.restDel(LPCSTR(url), statusCode);
		}
		image.Load(_T("success.png"));
		CRect imgRect;
		GetDlgItem(IDC_PStatus)->GetClientRect(&imgRect);
		CDC* picDC = GetDlgItem(IDC_PStatus)->GetDC();

		if (picDC != NULL)
		{
			image.Draw(picDC->m_hDC, imgRect);
			ReleaseDC(picDC);
		}
	}
	catch (std::exception & e)
	{
		MessageBox(e.what(), NULL, MB_OK);
		image.Load(_T("error.png"));
		CRect imgRect;
		GetDlgItem(IDC_PStatus)->GetClientRect(&imgRect);
		CDC* picDC = GetDlgItem(IDC_PStatus)->GetDC();

		if (picDC != NULL)
		{
			image.Draw(picDC->m_hDC, imgRect);
			ReleaseDC(picDC);
		}
	}
	itoa(statusCode, c, 10);
	GetDlgItem(IDC_Status)->SetWindowText(c);
}

void CRESTMFCDemoDlg::OnItemdblclickLctemplate(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMHEADER phdr = reinterpret_cast<LPNMHEADER>(pNMHDR);
	// TODO: Add your control notification handler code here
	*pResult = 0;
}

/** unicode转码，针对中文乱码 **/
char* CRESTMFCDemoDlg::U2G(const char* utf8)
{
	int len = MultiByteToWideChar(CP_UTF8, 0, utf8, -1, NULL, 0);
	wchar_t* wstr = new wchar_t[len+1];
	memset(wstr, 0, len+1);
	MultiByteToWideChar(CP_UTF8, 0, utf8, -1, wstr, len);
	len = WideCharToMultiByte(CP_ACP, 0, wstr, -1, NULL, 0, NULL, NULL);
	char* str = new char[len+1];
	memset(str, 0, len+1);
	WideCharToMultiByte(CP_ACP, 0, wstr, -1, str, len, NULL, NULL);
	if(wstr) delete[] wstr;
	return str;
}

void CRESTMFCDemoDlg:: Replace( std::string &strBig, const std::string &strsrc, const std::string &strdst)
{
	std::string::size_type pos = 0;
	std::string::size_type srclen = strsrc.size();
	std::string::size_type dstlen = strdst.size();

	while( (pos=strBig.find(strsrc, pos)) != std::string::npos )
	{
		strBig.replace( pos, srclen, strdst );
		pos += dstlen;
	}
}

void CRESTMFCDemoDlg::RefreshTemplate()
{
	DWORD dwStyle = m_Template.GetExtendedStyle();
	dwStyle |= LVS_EX_FULLROWSELECT;
	dwStyle |= LVS_EX_GRIDLINES;
	m_Template.SetExtendedStyle(dwStyle);
	m_Template.InsertColumn(0, _T("内容"), LVCFMT_LEFT, 200);

	ifstream ifs;
	ifs.open("template.json",ios::binary);

	if (!ifs.is_open())
	{
		CString messageHeader = _T("打开json模板文件失败:");
		CString message = messageHeader + "template.json";
		MessageBox(message, NULL, MB_OK);
		return;
	}
	Json::Reader reader;
	Json::Value root;
	if (reader.parse(ifs, root))
	{
		m_templateArray = root;
		for (int i = 0; i < root.size(); i++)
		{
			string name = U2G(root[i]["name"].asString().c_str());
			CString strItem = _T("");
			strItem.Format(_T(" %d"), i);
			m_Template.InsertItem(i, strItem);
			m_Template.SetItemText(i, 0, name.c_str());
		}
	}
}



void CRESTMFCDemoDlg::OnDblclkLctemplate(NMHDR *pNMHDR, LRESULT *pResult)
{
	LPNMITEMACTIVATE pNMItemActivate = reinterpret_cast<LPNMITEMACTIVATE>(pNMHDR);
	// TODO: 在此添加控件通知处理程序代码
	*pResult = 0;
	NM_LISTVIEW* pNMListView = (NM_LISTVIEW*)pNMHDR;
	int m_Row = pNMListView->iItem;
	m_method.SelectString(0,m_templateArray[m_Row]["method"].asString().c_str());
	GetDlgItem(IDC_EUrl)->SetWindowText(m_templateArray[m_Row]["url"].asString().c_str());
	string data = m_templateArray[m_Row]["data"].toStyledString();
	Replace(data,"\\\"","");
	CString requestValue = data.c_str();
	GetDlgItem(IDC_Request)->SetWindowText(requestValue);
	RefreshRequest();
}


void CRESTMFCDemoDlg::OnCbnSelchangeCbmethod()
{
	// TODO: 在此添加控件通知处理程序代码
	RefreshRequest();
}

void CRESTMFCDemoDlg::RefreshRequest()
{
	CString method;
	int index = m_method.GetCurSel();
	m_method.GetLBText(index,method);
	if (method == "GET"|| method == "DELETE")
	{
		GetDlgItem(IDC_Request)->EnableWindow(false);
	}
	else
	{
		GetDlgItem(IDC_Request)->EnableWindow(true);
	}
}
