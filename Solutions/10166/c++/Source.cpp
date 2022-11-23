// 2018-06-24

#include <iostream>
#include <string>

using namespace std;

#define NUMER_PERSONS 3

#define KEYVOON_PERION 6
#define NEZAM_PERION 3
#define SHIRFARHAD_PERION 4

struct SPerson
{
	string _name;
	int _period;
	string _pattern;
	int _correctAnswer;
	void set(string name, int period, string pattern, int correctAnswer = 0);
};

//Prototypes:
void checkAnswers(SPerson*, string);
void sortPersons_correctAnswer(SPerson*);
void printMaximums(SPerson*);

int main()
{
	//Definition and set:
	SPerson persons[NUMER_PERSONS];
	persons[0].set("keyvoon", KEYVOON_PERION, "331122");
	persons[1].set("nezam", NEZAM_PERION, "123");
	persons[2].set("shir farhad", SHIRFARHAD_PERION, "2123");

	//get num of answers:
	int n;
	cin >> n;

	//get answers:
	string answers = "";
	do
	{
		getline(cin, answers);
	} while (answers == "");

	checkAnswers(persons, answers);

	printMaximums(persons);
	return 0;
}

void SPerson::set(string name, int period, string pattern, int correctAnswer)
{
	_name = name;
	_period = period;
	_pattern = pattern;
	_correctAnswer = correctAnswer;
}

void checkAnswers(SPerson* persons, string answers)
{
	for (int i = 0; i < answers.length(); i++)
		for (int j = 0; j < NUMER_PERSONS; j++)
			if (answers.at(i) == persons[j]._pattern.at(i%persons[j]._period))
				persons[j]._correctAnswer++;
}

void sortPersons_correctAnswer(SPerson* persons)
{
	for (int i = 0; i < NUMER_PERSONS; i++)
	{
		for (int j = i + 1; j < NUMER_PERSONS - 1; j++)
		{
			if ((persons[i]._correctAnswer < persons[j]._correctAnswer) ||
				(persons[i]._correctAnswer == persons[j]._correctAnswer &&
					persons[i]._name > persons[j]._name))
			{
				auto temp = persons[i];
				persons[i] = persons[j];
				persons[j] = temp;
			}
		}
	}
}

void printMaximums(SPerson* persons)
{
	sortPersons_correctAnswer(persons);

	auto maximum = persons[0]._correctAnswer;
	cout << maximum << endl;

	for (int i = 0; i < NUMER_PERSONS; i++)
		if (persons[i]._correctAnswer == maximum)
			cout << persons[i]._name << endl;
}
