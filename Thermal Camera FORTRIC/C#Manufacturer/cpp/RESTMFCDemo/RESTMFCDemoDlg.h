
// RESTMFCDemoDlg.h : header file
//

#pragma once
#include "json/json.h"


// CRESTMFCDemoDlg dialog
class CRESTMFCDemoDlg : public CDialogEx
{
// Construction
public:
	CRESTMFCDemoDlg(CWnd* pParent = nullptr);	// standard constructor

// Dialog Data
#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_RESTMFCDEMO_DIALOG };
#endif

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;
	Json::Value m_templateArray;
	void RefreshTemplate();
	void RefreshRequest();
	char* U2G(const char* s);
	void Replace( std::string &strBig, const std::string &strsrc, const std::string &strdst);
	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedBsend();
	afx_msg void OnItemdblclickLctemplate(NMHDR *pNMHDR, LRESULT *pResult);
	CListCtrl m_Template;
	afx_msg void OnDblclkLctemplate(NMHDR *pNMHDR, LRESULT *pResult);
	CComboBox m_method;
	CEdit m_url;
	CIPAddressCtrl m_IPAddress;
	CString m_UserValue;
	CString m_PasswordValue;
	CString m_UrlValue;
	CString m_RequestValue;
	CString m_ResponseValue;
	CRichEditCtrl m_Request;
	CRichEditCtrl m_Response;
	CStatic m_PStatus;
	afx_msg void OnCbnSelchangeCbmethod();
};
