using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using Tic_Tac_Toe;
using Tic_Tac_Toe.Shared;

namespace Tic_Tac_Toe.Pages
{
    public partial class Index
    {
        string[] board = { "", "", "", "", "", "", "", "", "" };
        string player = "X";
        int[][] winningCombos =
        {
            new int[3] {0,1,2},
            new int[3] {3,4,5},
            new int[3] {6,7,8},
            new int[3] {0,3,6},
            new int[3] {1,4,7},
            new int[3] {2,5,8},
            new int[3] {0,4,8},
            new int[3] {2,4,6}
        };

        private async Task SquareCliked(int idx)
        {
            board[idx] = player;
            player = player == "X" ? "O" : "X";

            foreach (int[] combo in winningCombos)
            {
                int p1 = combo[0];
                int p2 = combo[1];
                int p3 = combo[2];
                if (board[p1] == String.Empty || board[p2] == String.Empty || board[p3] == String.Empty) continue;
                if (board[p1] == board[p2] && board[p2] == board[p3] && board[p1] == board[p3]) 
                {
                    string winner = player == "X" ? "Player TWO" : "Player ONE";
                    await JS.InvokeVoidAsync("ShowSwal", winner);
                    ResetGame();
                }
            }

            if (board.All(x => x != ""))
            {
                await JS.InvokeVoidAsync("ShowTie");
                ResetGame();
            }
        }

        private void ResetGame()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = "";
            }
        }
    }
}
