#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "shuffle_test.h"
#include "shuffle_answer.h"

int main(int argc, char const *argv[])
{
    srand(time(NULL));

    if (argc == 1)
    {
        N = 0;
        int poker[54];
        int i, j;
        for (j = 0; j < 54; j++)
        {
            poker[j] = j;
        }
        shuffle_answer(poker);
    }
    else
    {
        N = atoi(argv[1]);
        shuffle_test(N);
    }

    // printf("%d\n", N);

    return 0;
}
