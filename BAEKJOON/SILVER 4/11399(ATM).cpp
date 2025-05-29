#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

bool comp(int a, int b)
{
	return a > b;
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n, input, beforeTime, sumTime = 0;
	vector<int> vInt;
	vector<int> vTime;

	cin >> n;

	for (int i = 0; i < n; i++)
	{
		cin >> input;
		vInt.push_back(input);
	}

	sort(vInt.begin(), vInt.begin() + vInt.size(), comp);

	for (int i = 0; i < n; i++)
	{
		if (vTime.empty())
		{
			beforeTime = 0;
		}
		else
		{
			beforeTime = vTime.back();
		}
		vTime.push_back(beforeTime + vInt.back());
		vInt.pop_back();
	}

	for (int i = 0; i < n; i++)
	{
		sumTime += vTime[i];
	}

	cout << sumTime;

	return 0;
}