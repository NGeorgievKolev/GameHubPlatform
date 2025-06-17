<template>
  <div class="container">
    <h1>Tic Tac Toe</h1>

    <button @click="createGame" class="new-game">New Game</button>

    <div v-if="game" class="status">
      <p v-if="game.isCompleted && game.winner">Winner: {{ game.winner }}</p>
      <p v-else-if="game.isCompleted">Draw!</p>
      <p v-else>Current Player: {{ game.currentPlayer }}</p>
    </div>

    <div v-if="game" class="board">
      <div
        v-for="(cell, index) in game.board"
        :key="index"
        @click="makeMove(index)"
        class="cell"
      >
        {{ cell === '-' ? '' : cell }}
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref } from 'vue';
import type { TicTacToeGame } from './types';

const game = ref<TicTacToeGame | null>(null);

async function createGame() {
  const res = await fetch('http://localhost:5002/api/game', { method: 'POST' });
  game.value = await res.json();
}

async function makeMove(position: number) {
  if (!game.value || game.value.isCompleted || game.value.board[position] !== '-') return;

  const res = await fetch(
    `http://localhost:5002/api/game/${game.value.sessionId}/move?position=${position}`,
    { method: 'POST' }
  );
  if (res.ok) {
    game.value = await res.json();
  }
}
</script>

<style scoped>
.container {
  max-width: 400px;
  margin: 2rem auto;
  padding: 1rem;
  text-align: center;
  font-family: Arial, sans-serif;
}

h1 {
  margin-bottom: 1rem;
}

.new-game {
  padding: 0.5rem 1rem;
  margin-bottom: 1rem;
  background-color: #2b7dfa;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.board {
  display: grid;
  grid-template-columns: repeat(3, 80px);
  gap: 8px;
  justify-content: center;
  margin-top: 1rem;
}

.cell {
  width: 80px;
  height: 80px;
  font-size: 2rem;
  border: 2px solid #333;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  user-select: none;
}

.cell:hover {
  background-color: #eee;
}

.status {
  font-size: 1rem;
  margin-bottom: 0.5rem;
}
</style>