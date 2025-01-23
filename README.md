# The Tower of London: Cognitive Puzzle Game
## Game Description:
This project is a digital implementation of the cognitive game "The Tower of London," often used in neuropsychological studies to assess planning, attention, and problem-solving abilities. The game is developed in Unity and incorporates modern asynchronous programming (UniTask) and reactive programming (UniRx) technologies.
##Technologies Used:
- **UniTask** - A library for working with asynchronous programming, such as handling level loading or implementing delays.
- **UniRx** (Reactive Extensions for Unity)* - A library for reactive programming, providing flexible event handling and simplifying game interaction logic.
- **File I/O** — For saving and loading the counter state.
## Gameplay Description:
- The game is based on the classic "The Tower of London" task: the player must move disks of different sizes between pegs to match a target configuration in the fewest possible moves.
- The game board consists of several pegs (3-4) and disks of varying sizes.
- Disks can only be stacked in order of decreasing size (smaller disks on top of larger ones).
- At the start of the game, the disks are placed in a random initial configuration. The player must carefully plan their moves to reach the target configuration while minimizing the number of moves.
- Mechanics: the player clicks on a disk to select it, then clicks on the target peg to move the disk there. The system ensures that moves comply with the rules (e.g., disks cannot be placed on smaller disks).
##Features:
- Simple and intuitive controls: click to select a disk, then click to choose the destination peg.
- Asynchronous operations to ensure a smooth gameplay experience (e.g., adding delays or animations during moves).
- Scoring system that tracks and displays the number of moves made.

[youtube](https://www.youtube.com/watch?v=s04T1BfJxaU)
