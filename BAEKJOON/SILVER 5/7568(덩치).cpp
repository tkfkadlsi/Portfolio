#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;

struct Human
{
	int height = 0;
	int weight = 0;
	int rank = 1;
};

bool comp(Human h1, Human h2)
{
	return h1.weight < h2.weight && h1.height < h2.height;
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n;
	Human input;
	vector<Human> vHuman;
	cin >> n;

	for (int i = 0; i < n; i++)
	{
		Human human;
		cin >> human.weight;
		cin >> human.height;
		vHuman.push_back(human);
	}

	for (int i = 0; i < vHuman.size(); i++)
	{
		for (int j = 0; j < vHuman.size(); j++)
		{
			if (comp(vHuman[i], vHuman[j]))
			{
				vHuman[i].rank++;
			}
		}
	}

	for (int i = 0; i < n; i++)
	{
		cout << vHuman[i].rank << '\n';
	}

	return 0;
}