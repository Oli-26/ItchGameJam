
extends Node

@export var scenes: Array[PackedScene] = []
@export var directionBias : float;
@export var randomness : float;
@export var minLengthRun : int;
@export var maxLengthRun : int;
@export var numberOfExtraPoints : int;
@export var mazeSize : int;
@export var chanceToSplitRoom : float = 20;
var validRoomShapes = [];
var validRommShapesOrdered = [];
var floorSection = preload("res://Scenes/Sections/Template.tscn")
const GridHelpers = preload("res://Source/MazeGenerator/GridHelpers.gd")
var player =  preload("res://Scenes/Player/player.tscn")
var doors = preload("res://Scenes/Sections/Doors.tscn")

var height = mazeSize;
var width = mazeSize;

var maze;

func withinGridBounds(x, y):
	if(x < 0 or x >= width or y < 0 or y >= height):
		return false;
	return true;

func setUpDoors(grid, doorGrid, x, y, i, j, roomSize):
	var northDoor = false;
	var southDoor = false;
	var eastDoor = false;
	var westDoor = false;
	if withinGridBounds(x+i+1, y+j) && grid[y+j][x+i+1] == 1:
		eastDoor = true;
	if withinGridBounds(x+i, y+j+1) && grid[y+j+1][x+i] == 1:
		southDoor = true;

	if withinGridBounds(x+i-1, y+j) && grid[y+j][x+i-1] == 1:
		westDoor = true;
	if withinGridBounds(x+i, y+j-1) && grid[y+j-1][x+i] == 1:
		northDoor = true;
	doorGrid[y+j][x+i] = {"northDoor": northDoor, "westDoor": westDoor, "eastDoor": eastDoor, "southDoor": southDoor};

		
			
func groupGridNumbers(grid):
	var groupedGrid = GridHelpers.createEmptyNullGrid(width, height);
	var doorGrid = GridHelpers.createEmptyNullGrid(width, height);
	for roomSize in validRommShapesOrdered:		
		for x in range(width):
			for y in range(height):
				if(groupedGrid[y][x] == null):						
					var failedToMatch = false;	
					for i in range(roomSize.X):
						for j in range(roomSize.Y):
							if !withinGridBounds(x+i, y+j) or grid[y+j][x+i] == 0 or groupedGrid[y+j][x+i] != null:
								failedToMatch = true;
					if roomSize != {"X": 1, "Y": 1} and GridHelpers.randomNumber(0, 100) < (chanceToSplitRoom):
						failedToMatch = true;
							
					if(!failedToMatch):
						for i in range(roomSize.X):
							for j in range(roomSize.Y):
								setUpDoors(grid, doorGrid, x, y, i, j, roomSize)
								groupedGrid[y+j][x+i] = roomSize;
								
	return {"groupedGrid": groupedGrid, "doorGrid": doorGrid};
			
func getValidRoomShapes():
	for room in scenes:
		var roomInst = room.instantiate();
		validRoomShapes.append({"X": roomInst.X, "Y": roomInst.Y})
		roomInst.queue_free();
	validRommShapesOrdered = validRoomShapes.duplicate()
	validRommShapesOrdered.sort_custom(Callable(self, "sortRoomSizes"))
	print(validRommShapesOrdered)

func sortRoomSizes(roomSize1, roomSize2):
		return (roomSize1.X + roomSize1.Y) > (roomSize2.X + roomSize2.Y)
		
func getRandomScene(roomSize):
	var randomNumber;
	var foundApplicableRoom = false;
	
	while(!foundApplicableRoom):
		randomNumber = GridHelpers.randomInt(0, scenes.size()-1);
		var roomToMake = scenes[randomNumber];
		var roomShape = validRoomShapes[randomNumber]
		
		if(roomShape.X == roomSize.X and roomShape.Y == roomSize.Y):
			return roomToMake.instantiate()
			

func spawnFloorSection(x, y, roomSize, filledGrid, doorsGrid):
	if(roomSize == null):
		return;
	var floorInst = getRandomScene(roomSize)
	floorInst.position.x = x*24;
	floorInst.position.y = 0;
	floorInst.position.z = y*24;
	self.add_child(floorInst);
	fillGridFromBy(filledGrid, x, y, roomSize, doorsGrid)
	
func fillGridFromBy(filledGrid, x, y, roomSize, doorsGrid):
	if(y == 14 or x == 14):
		return #This is a bug from somewhere that should be fixed really
	filledGrid[y][x] = 1;
	for xOffset in range(roomSize.X):
		for yOffset in range(roomSize.Y):
			filledGrid[y + yOffset][x + xOffset] = 1;
			if should_spawn_doors(x, y):
				spawnDoors(x + xOffset, y + yOffset, doorsGrid[y + yOffset][x + xOffset]);

func should_spawn_doors(x, y):
	return !(x == 0 and y == 0) and !(x == mazeSize-1 and y == mazeSize-1)

func spawnDoors(x, y, doorsDict):
	var doorsInst = doors.instantiate()
	doorsInst.position.x = x*24;
	doorsInst.position.y = 0;
	doorsInst.position.z = y*24;
	self.add_child(doorsInst);
	doorsInst.disableDoors(doorsDict.northDoor, doorsDict.southDoor, doorsDict.eastDoor, doorsDict.westDoor);
	
func _ready():
	height = mazeSize;
	width = mazeSize;
	getValidRoomShapes();
	maze = GridHelpers.createMultiCheckpointMaze(mazeSize, mazeSize, directionBias, randomness, minLengthRun, maxLengthRun, numberOfExtraPoints);
	var filledGrid = GridHelpers.createEmptyGrid(mazeSize, mazeSize)
	var groupings = groupGridNumbers(maze.grid);
	var groupedGrid = groupings.groupedGrid;
	var doorGrid = groupings.doorGrid;
	#var playerInst = player.instantiate()
	#playerInst.position = Vector3(24*maze.startPosition.x + 12, 3, 24*maze.startPosition.y + 12);	
	#self.add_child(playerInst);
	for x in range(width):
		for y in range(height):
			if(maze.grid[y][x] == 1 && filledGrid[y][x] == 0):
				spawnFloorSection(x, y, groupedGrid[y][x], filledGrid, doorGrid);



