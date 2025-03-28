
// VIDEOMFCDemoDlg.h : 头文件
//

#pragma once
#include <string>
#include <cstdint>
#include "../../sdk/StreamSDK.h"
#pragma comment(lib,"../../sdk/streamsdk.lib")

#include "IRCamera.h"
#include "GridCtrl_src/GridCtrl.h"

/** 临界区 **/
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

// CVIDEOMFCDemoDlg 对话框
class CVIDEOMFCDemoDlg : public CDialogEx
{
// 构造
public:
	CVIDEOMFCDemoDlg(CWnd* pParent = NULL);	// 标准构造函数

// 对话框数据
	enum { IDD = IDD_VIDEOMFCDEMO_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
	IRCamera * m_Camera;
	afx_msg LRESULT OnUpdateTable(WPARAM wParam, LPARAM lParam);
public:
	afx_msg void OnBnClickedBconnect();
	afx_msg void OnBnClickedBconnect2();
private:
	CRITICAL_SECTION m_RawBuf_cs;
	static void CALLBACK Grabber(int error, streamsdk_st_buffer * buffer, void * user_data);
public:
	CIPAddressCtrl m_IPAddress;
	CString m_UserValue;
	CString m_PasswordValue;
	int m_RegionHeightValue;
	int m_RegionWidthValue;
	int m_RegionXValue;
	int m_RegionYValue;
	CGridCtrl m_Grid;
	void GridCtrlInit();
	afx_msg void OnClose();
};
