
// TAGMFCDemo.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CTAGMFCDemoApp:
// �йش����ʵ�֣������ TAGMFCDemo.cpp
//

class CTAGMFCDemoApp : public CWinApp
{
public:
	CTAGMFCDemoApp();

// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CTAGMFCDemoApp theApp;