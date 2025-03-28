
// TAGMFCDemoDlg.cpp : ʵ���ļ�
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
// CTAGMFCDemoDlg �Ի���


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


// CTAGMFCDemoDlg ��Ϣ�������

BOOL CTAGMFCDemoDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// ���ô˶Ի����ͼ�ꡣ��Ӧ�ó��������ڲ��ǶԻ���ʱ����ܽ��Զ�
	//  ִ�д˲���
	SetIcon(m_hIcon, TRUE);			// ���ô�ͼ��
	SetIcon(m_hIcon, FALSE);		// ����Сͼ��

	// TODO: �ڴ���Ӷ���ĳ�ʼ������
	InitializeCriticalSection(&m_TagBuf_cs);
	GridCtrlInit();
	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
}

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CTAGMFCDemoDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // ���ڻ��Ƶ��豸������

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// ʹͼ���ڹ����������о���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// ����ͼ��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

//���û��϶���С������ʱϵͳ���ô˺���ȡ�ù��
//��ʾ��
HCURSOR CTAGMFCDemoDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

void CTAGMFCDemoDlg::GridCtrlInit()
{
	m_Grid.SetEditable(false);
	m_Grid.SetTextBkColor(RGB(0xFF, 0xFF, 0xE0));//��ɫ����
	m_Grid.SetRowCount(8);
	m_Grid.SetColumnCount(4);
	m_Grid.SetFixedRowCount(1); //��ͷΪһ��
	m_Grid.SetItemText(0,0,_T("���������"));
	m_Grid.SetItemText(0,1,_T("�����"));
	m_Grid.SetItemText(0,2,_T("�����"));
	m_Grid.SetItemText(0,3,_T("ƽ����"));
}

void CTAGMFCDemoDlg::OnBnClickedBconnect()
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	if (m_Camera)
	{
		delete m_Camera;
		GetDlgItem(IDC_BConnect)->SetWindowText("����");
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
			GetDlgItem(IDC_BConnect)->SetWindowText("����");
			AfxMessageBox(e.what());
			return;
		}

		try
		{
			m_Camera->StartStream(CTAGMFCDemoDlg::Grabber,this);
			GetDlgItem(IDC_BConnect)->SetWindowText("�Ͽ�");
		}
		catch(std::exception & e)
		{
			delete m_Camera;
			m_Camera = NULL;

			GetDlgItem(IDC_BConnect)->SetWindowText("����");
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
	m_Grid.SetItemText(1,0,_T("ȫ��"));
	char maxValue[] = "";
	sprintf(maxValue, "%.1f ��", tagValues["max"]["t"].asFloat());
	m_Grid.SetItemText(1,1,maxValue);
	char minValue[] = "";
	sprintf(minValue, "%.1f ��", tagValues["min"]["t"].asFloat());
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
				sprintf(sub_maxValue, "%.1f ��", tagValues[name]["max"]["t"].asFloat());
				m_Grid.SetItemText(rowIndex,1,sub_maxValue);
				char sub_minValue[10] = "";
				sprintf(sub_minValue, "%.1f ��", tagValues[name]["min"]["t"].asFloat());
				m_Grid.SetItemText(rowIndex,2,sub_minValue);
				m_Grid.SetItemText(rowIndex,3,_T("-"));
			}
			else
			{
				m_Grid.SetItemText(rowIndex,1,_T("-"));
				m_Grid.SetItemText(rowIndex,2,_T("-"));
				char value[10] = "";
				sprintf(value, "%.1f ��", value);
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
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ

	CDialogEx::OnClose();
	if (m_Camera)
	{
		delete m_Camera;
	}
}
