#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

struct COORD
{
	int x = 0;
	int y = 0;
};

bool comp(COORD c1, COORD c2)
{
	if (c1.y == c2.y)
	{
		return c1.x < c2.x;
	}
	else
	{
		return c1.y < c2.y;
	}
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n;
	vector<COORD> vCoord;
	vCoord.reserve(100000);

	cin >> n;

	for (int i = 0; i < n; i++)
	{
		COORD c;
		cin >> c.x >> c.y;
		vCoord.push_back(c);
	}

	sort(vCoord.begin(), vCoord.begin() + vCoord.size(), comp);

	for (int i = 0; i < n; i++)
	{
		cout << vCoord[i].x << " " << vCoord[i].y << '\n';
	}

	return 0;
}