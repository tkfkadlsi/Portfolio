#pragma once
#include <vector>
typedef struct _tagpos
{
	int x;
	int y;
	bool operator == (const _tagpos& other)
	{
		if (x == other.x && y == other.y) return true;
		else return false;
	}
}POS, *PPOS;

typedef struct _tagplayer
{
	POS position;
	//countMoveTime�� �� ���ں��� ũ�� �̵� ����.
	long waitMSForMove = 100;

	//update���� �Ź� deltatime��ŭ ����������
	long countMoveTime = 0;

	//superGuardTime�� 0�ʰ��� ��������, update���� �Ź� ���ҽ�����.
	long superGuardTime = 0;

	//��Ÿ���� ������ �ִ� ����
	long superGuardCoolTime = 5000;

	//countSuperGuardCoolDown�� �� �������� ũ�� ���� ��밡��, update���� �Ź� ����������.
	long countSuperGuardCoolDown = 5000;
}PLAYER, *PPLAYER;

typedef struct _tagarrow
{
	POS position; //ȭ��ǥ�� ��ġ
	int spawnDir; //ȭ��ǥ�� ����
	long countwaitTime; //����������� �ð�
	bool isBombed = false; //�������� �ƴ���
	long countAfterBombTime; //������ ������������ �ð�

	bool operator==(_tagarrow other) // == ������
	{
		return
			position == other.position &&
			spawnDir == other.spawnDir &&
			countwaitTime == other.countwaitTime &&
			isBombed == other.isBombed &&
			countAfterBombTime == countAfterBombTime;
	}
}ARROW, *PARROW;

void Frame(int frame, PPLAYER pPlayer, long* deltaTime);
bool Update(char map[8][8], PPLAYER pPlayer, long* deltaTime, bool isStart = false);
void Render(char map[8][8], PPLAYER pPlayer, time_t currentTime);
void BorderRender(int mapSize);
void MoveUpdate(char map[8][8], PPLAYER pPlayer);
void CreateArrow(char map[8][8], PPLAYER pPlayer, std::vector<ARROW>& arrowVec, COORD mapStart, int* waitCreateArrow, long* deltaTime);
void ActiveArrow(char map[8][8], std::vector<ARROW>& arrowVec, COORD mapStart, long* deltaTime);
void DeleteArrow(char map[8][8], std::vector<ARROW>& arrowVec, COORD mapStart, long* deltaTime);
void ResetArrow(std::vector<ARROW>& arrowVec);