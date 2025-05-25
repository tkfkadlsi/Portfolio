#include <iostream>
#include <vector>
#include <algorithm>
#include <math.h>
using namespace std;

bool comp1(int i1, int i2)
{
	return i1 > i2;
}

bool comp2(int i1, int i2)
{
	return i1 < i2;
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n, input, cut, sum = 0, average = 0;
	vector<int> vInt;
	vInt.reserve(300000);
	cin >> n;

	if (n == 0)
	{
		cout << 0;
		return 0;
	}

	for (int i = 0; i < n; i++)
	{
		cin >> input;
		vInt.push_back(input);
	}

	cut = (int)floor(((float)vInt.size() * 0.15f) + 0.5f);

	sort(vInt.begin(), vInt.begin() + vInt.size(), comp1);

	for (int i = 0; i < cut; i++)
	{
		vInt.pop_back();
	}

	sort(vInt.begin(), vInt.begin() + vInt.size(), comp2);

	for (int i = 0; i < cut; i++)
	{
		vInt.pop_back();
	}

	for (int i = 0; i < vInt.size(); i++)
	{
		sum += vInt[i];
	}

	average = (int)floor(((float)sum / (float)vInt.size()) + 0.5f);

	cout << average;

	return 0;
}