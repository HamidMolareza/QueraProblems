#include <iostream>
#include <vector>
#include <string>

std::vector<std::string> get_inputs(int num_of_lines)
{
    std::vector<std::string> inputs(num_of_lines);
    for (int i = 0; i < num_of_lines; i++)
    {
        std::getline(std::cin, inputs[i]);
    }
    return inputs;
}

int main()
{
    // Get inputs
    const int NUM_OF_INPUTS = 5;
    auto inputs = get_inputs(NUM_OF_INPUTS);

    // Find indexes
    std::vector<int> indexes;
    for (int i = 0; i < NUM_OF_INPUTS; i++)
    {
        if (inputs[i].find("FBI") != std::string::npos)
        {
            indexes.push_back(i + 1);
        }
    }

    // Print
    if (indexes.size() > 0)
    {
        for (int i = 0; i < indexes.size(); i++)
        {
            std::cout << indexes[i] << " ";
        }
        std::cout << std::endl;
    }
    else
    {
        std::cout << "HE GOT AWAY!" << std::endl;
    }

    return 0;
}
