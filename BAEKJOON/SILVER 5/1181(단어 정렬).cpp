#include <iostream>
#include <vector>
#include <queue>
#include <unordered_set>
#include <algorithm>
using namespace std;

bool SORT(string s1, string s2)
{
	return s1 < s2;
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	vector<string> vstr;
	queue<string> qstr[50];
	unordered_set<string> usstr;
	string s;
	int n;

	cin >> n;

	for (int i = 0; i < n; i++)
	{
		cin >> s;
		if (usstr.find(s) == usstr.end())
		{
			usstr.insert(s);
			vstr.push_back(s);
		}
	}

	sort(vstr.begin(), vstr.begin() + vstr.size(), SORT);

	for (int i = 0; i < vstr.size(); i++)
	{
		qstr[vstr[i].size() - 1].push(vstr[i]);
	}

	for (int i = 0; i < 50; i++)
	{
		while (!qstr[i].empty())
		{
			cout << qstr[i].front() << '\n';
			qstr[i].pop();
		}
	}

	return 0;
}