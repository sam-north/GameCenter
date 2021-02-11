import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'board',
    templateUrl: './board.component.html',
    styles: [
        `main {display: grid;
    grid-template-columns: 200px 200px 200px;}`
    ]
})

export class BoardComponent implements OnInit {
    squares: any[];
    xIsNext: boolean;
    winner: string;

    constructor() {}

    ngOnInit() {
        this.newGame();
    }

    newGame() {
        this.squares = Array(9).fill(null);
        this.winner = null;
        this.xIsNext = true;
    }

    get player() {
        return this.xIsNext ? 'X' : 'O';
    }

    // method serves as event handler when click a button
    // check index in the array they've clicked on
    // if already clicked do nothing
    // if null, splice in the index of the square with the player (x or o)
    makeMove(index: number) {
        if (!this.squares[index]) {
            this.squares.splice(index, 1, this.player)
            this.xIsNext = !this.xIsNext;
        }

        this.winner = this.calculateWinner();
    }

    calculateWinner() {
        const lines = [
            [0, 1, 2],
            [3, 4, 5],
            [6, 7, 8],
            [0, 3, 6],
            [1, 4, 7],
            [2, 5, 8],
            [0, 4, 8],
            [2, 4, 6]
        ];
        for (let i = 0; i < lines.length; i++) {
            const [a, b, c] = lines[i];
            if (
                this.squares[a] &&
                this.squares[a] === this.squares[b] &&
                this.squares[a] === this.squares[c]
            ) {
                return this.squares[a]
            }
        }
        return null;
    }

}