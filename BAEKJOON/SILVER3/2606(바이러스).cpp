#include <iostream>
#include <vector>
using namespace std;

int infectedCount = -1;

class Computer
{
public:
	void Infection(Computer comArray[])
	{
		if (isInfected) return;

		isInfected = true;
		infectedCount++;

		for (int i = 0; i < linkedComputer.size(); i++)
		{
			comArray[linkedComputer[i]].Infection(comArray);
		}
	}

	void Link(int i)
	{
		linkedComputer.push_back(i);
	}

private:
	vector<int> linkedComputer;
	bool isInfected = false;
};


int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	Computer comArray[101];

	int n, m, a, b;
	cin >> n;
	cin >> m;

	for (int i = 0; i < m; i++)
	{
		cin >> a >> b;

		comArray[a].Link(b);
		comArray[b].Link(a);
	}

	comArray[1].Infection(comArray);

	cout << infectedCount;

	return 0;
}