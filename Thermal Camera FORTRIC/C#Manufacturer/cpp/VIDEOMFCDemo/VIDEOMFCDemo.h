
// VIDEOMFCDemo.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CVIDEOMFCDemoApp:
// �йش����ʵ�֣������ VIDEOMFCDemo.cpp
//

class CVIDEOMFCDemoApp : public CWinApp
{
public:
	CVIDEOMFCDemoApp();

// ��д
public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CVIDEOMFCDemoApp theApp;