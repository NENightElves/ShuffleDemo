#include <stdlib.h>
#include <time.h>
#include "shuffle_test.h"
#include "shuffle_answer.h"

void shuffle_answer(int poker[])
{
    int i, k, t;
    print_step(poker);
    for (i = 0; i < 54; i++)
    {
        k = rand() % (54 - i) + i;
        t = poker[i];
        poker[i] = poker[k];
        poker[k] = t;
        print_step(poker);
    }
}
