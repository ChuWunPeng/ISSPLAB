
// DLMFCDemoDlg.h : ͷ�ļ�
//

#pragma once
#include <string>
#include <cstdint>
#include "../../sdk/StreamSDK.h"
#pragma comment(lib,"../../sdk/streamsdk.lib")

#include "IRCamera.h"
#include "palettes.h"


/** ͼƬ���� **/
class SimpleImageBuffer
{
public:
	SimpleImageBuffer();
	~SimpleImageBuffer();

	void resize(std::size_t sz);
	void copyImage(void * ptr,std::size_t sz);
	BYTE * getImage();

	int m_Width;
	int m_Height;
	int m_Size;
private:
	BYTE * m_ImageBuf;
	std::size_t m_ImageBufSize;
};

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

// CDLMFCDemoDlg �Ի���
class CDLMFCDemoDlg : public CDialogEx
{
// ����
public:
	CDLMFCDemoDlg(CWnd* pParent = NULL);	// ��׼���캯��

    // �Ի�������
	enum { IDD = IDD_DLMFCDEMO_DIALOG };

	/** ��ͼ���� **/
	enum ViewType
	{ 
		RawView = 0,		/** ȫ��������ͼ **/
		H264View = 1		/** ѹ��������ͼ **/
	};

protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��
	
	struct ThreadInfo{
		CStatic *tempCtrl;
		std::string temp;
	};

// ʵ��
protected:
	HICON m_hIcon;
	
	void DisableView();
	void SwitchView(ViewType v);
	BOOL SwitchH264Stream(IRCamera::stream_type);

	// ���ɵ���Ϣӳ�亯��
	DECLARE_MESSAGE_MAP()
	
	
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();

	IRCamera * m_Camera;
	afx_msg void OnBnClickedBconnect();
	afx_msg void OnBnClickedBrawsnap();
	afx_msg void OnBnClickedBh264snap();
	afx_msg void OnBnClickedBautofocus();
	afx_msg void OnBnClickedRh264();
	afx_msg void OnBnClickedRraw();
	afx_msg void OnBnClickedRpri();
	afx_msg void OnBnClickedRsub();
	afx_msg void OnCbnSelchangeCbpalette();
	afx_msg void OnTimer(UINT_PTR nIDEvent);
	afx_msg void OnShowWindow(BOOL bShow, UINT nStatus);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnClose();
	afx_msg LRESULT OnUpdateTemperature(WPARAM wParam, LPARAM lParam);  
	afx_msg LRESULT OnUpdateImage(WPARAM wParam, LPARAM lParam);  
private:
	void DisplayImage(std::string paletteKey,unsigned short * rawValues);
	void ShowImage();
	void SnapH264(const char* buf);
	void SnapRaw(const char* buf);
	
	static UINT _acquireTemperature(LPVOID pthread);
	static void CALLBACK GrabberRaw(int error, streamsdk_st_buffer * buffer, void * user_data);
	static void CALLBACK GrabberH264(int error,streamsdk_st_image *img,void * user_data);

	CStatic m_Image;
	
	CIPAddressCtrl m_IPAddress;
	CString m_PasswordValue;
	CString m_UserValue;
	ViewType m_CurrentView;
	IRCamera::stream_type m_StreamType;

	float m_wRatio;
	float m_hRatio;
	
	CRITICAL_SECTION m_MousemovePoint_cs;
	CPoint m_MousemovePoint;

	
	CComboBox m_Palettes;
	//CStatic m_TempValue;

	uint8_t (* m_RawPalette)[3];

	CRITICAL_SECTION m_ImageBuf_cs;
	SimpleImageBuffer m_ImageBuf;

	CWinThread* m_TempAcqThread;
	BOOL m_TempAcqClose;
	float m_TempAcq;
	CString m_defaultSavePath;
public:
	afx_msg void OnClickedBrawsnap();
	afx_msg void OnClickedBh264snap();
};