#include <iostream>
#include <vector>
#include <algorithm>
using namespace std;


struct OnlineMember
{
	string name = "";
	int age = 0;
	int joinCount = 0;
};


bool comp(OnlineMember om1, OnlineMember om2)
{
	if (om1.age == om2.age)
	{
		return om1.joinCount < om2.joinCount;
	}
	else
	{
		return om1.age < om2.age;
	}
}

int main()
{
	ios_base::sync_with_stdio(0);
	cin.tie(0);

	int n;
	vector<OnlineMember> vOm;
	vOm.reserve(100000);

	cin >> n;

	for (int i = 0; i < n; i++)
	{
		OnlineMember om;
		cin >> om.age >> om.name;
		om.joinCount = i;
		
		vOm.push_back(om);
	}

	sort(vOm.begin(), vOm.begin() + vOm.size(), comp);

	for (int i = 0; i < n; i++)
	{
		cout << vOm[i].age << " " << vOm[i].name << '\n';
	}

	return 0;
}