
// TAGMFCDemoDlg.h : ͷ�ļ�
//

#pragma once
#include <string>
#include <cstdint>
#include "../../sdk/StreamSDK.h"
#pragma comment(lib,"../../sdk/streamsdk.lib")

#include "IRCamera.h"
#include "GridCtrl_src/GridCtrl.h"

/** �ٽ��� **/
class CSLock
{
public:
	CSLock(CRITICAL_SECTION *cs)
		:_cs(cs)
	{
		EnterCriticalSection(_cs);
	}

	~CSLock()
	{
		LeaveCriticalSection(_cs);
	}
private:
	CRITICAL_SECTION * _cs;
};

// CTAGMFCDemoDlg �Ի���
class CTAGMFCDemoDlg : public CDialogEx
{
// ����
public:
	CTAGMFCDemoDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
	enum { IDD = IDD_TAGMFCDEMO_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��


// ʵ��
protected:
	HICON m_hIcon;

	// ���ɵ���Ϣӳ�亯��
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
	IRCamera * m_Camera;
	afx_msg LRESULT OnUpdateTag(WPARAM wParam, LPARAM lParam);
public:
	afx_msg void OnBnClickedBconnect();
private:
	CRITICAL_SECTION m_TagBuf_cs;
	static void CALLBACK Grabber(int error, streamsdk_st_buffer * buffer, void * user_data);
	void UpdateTag();
public:
	CIPAddressCtrl m_IPAddress;
	CString m_UserValue;
	CString m_PasswordValue;
	CGridCtrl m_Grid;
	void GridCtrlInit();
	afx_msg void OnClose();
};
