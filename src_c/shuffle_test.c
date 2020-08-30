#include "shuffle_test.h"
#include "shuffle_answer.h"
#include <stdio.h>

void print_step(int poker[])
{
    if (N > 0)
        return;
    for (int i = 0; i < 54; i++)
    {
        printf("%d ", poker[i]);
    }
    printf("\n");
}

void shuffle_test(int n)
{
    int poker[54];
    int i, j;
    for (i = 0; i < n; i++)
    {
        for (j = 0; j < 54; j++)
        {
            poker[j] = j;
        }
        shuffle_answer(poker);
        for (int i = 0; i < 54; i++)
        {
            printf("%d ", poker[i]);
        }
        printf("\n");
    }
}