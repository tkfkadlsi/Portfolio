#include <iostream>
#include <map>
#include <vector>
#include <algorithm>
using namespace std;

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	map<string, int> hearAndSeeMap;
	string input;

	int n, m, count = 0;
	vector<string> vStr;

	cin >> n >> m;

	for (int i = 0; i < n + m; i++)
	{
		cin >> input;
		
		if (hearAndSeeMap.find(input) == hearAndSeeMap.end())
		{
			hearAndSeeMap.insert({input, 1});
		}
		else
		{
			hearAndSeeMap[input]++;
		}
	}
	
	for (auto m : hearAndSeeMap)
	{
		if (m.second > 1)
		{
			count++;
			vStr.push_back(m.first);
		}
	}

	sort(vStr.begin(), vStr.begin() + vStr.size());

	cout << count << '\n';

	for (int i = 0; i < vStr.size(); i++)
	{
		cout << vStr[i] << '\n';
	}

	return 0;
}