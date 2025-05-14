#include "pch.h"
#include "Background.h"
#include "ResourceManager.h"
#include "Texture.h"
Background::Background()
{

}

Background::~Background()
{
}

void Background::Init()
{
}

void Background::Update()
{
}

void Background::Render(HDC _hdc)
{
	int width = pTex->GetWidth();
	int height = pTex->GetHeight();
	::BitBlt(_hdc
	, SCREEN_WIDTH / 2 - width / 2
	, SCREEN_HEIGHT / 2 - height / 2
	, width, height,
		pTex->GetTexDC()
	,0,0,SRCCOPY
);
	//::BitBlt(); // �׳� �ű涧 - ��Ӻ���
	//::TransparentBlt(); // ������ ����
	//::StretchBlt(); // Ȯ�� Ȥ�� ����ؼ� �׸���, �����Ҷ�
}

void Background::SetBG(Texture* texture)
{
	pTex = texture;
}
