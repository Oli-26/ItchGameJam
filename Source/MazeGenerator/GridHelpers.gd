static func randomNumber(lowest, highest):
	var rng = RandomNumberGenerator.new()
	rng.randomize()
	return rng.randf_range(lowest, highest)
	
static func randomInt(lowest, highest):
	var rng = RandomNumberGenerator.new()
	rng.randomize()
	return rng.randi_range(lowest, highest)
	
static func getUnitDifference(to, from):
	if(to.x - from.x > 0):
		if(to.y - from.y > 0):
			return Vector2(1, 1);
		else:
			return Vector2(1,-1);
	else:
		if(to.y - from.y > 0):
			return Vector2(-1, 1);
		else:
			return Vector2(-1,-1);
			
static func isCloseTo(to, from, within):
	if abs(to.x - from.x) <= within && abs(to.y - from.y) <= within:
		return true;
	return false;	
	
static func randomMovement(multiplier, towardsMultiplier, xBias, randomness):
	var changeX = randomInt(0, 100) < (50 + xBias);
	var towards = randomInt(0, 100) < (100-(randomness/2.0));
	if changeX:
		var movementMagnitute = multiplier * towardsMultiplier.x;
		if !towards:
			movementMagnitute = -movementMagnitute;
		return Vector2(movementMagnitute, 0);
	else:
		var movementMagnitute = multiplier * towardsMultiplier.y;
		if !towards:
			movementMagnitute = -movementMagnitute;
		return Vector2(0, movementMagnitute);
		
static func isWithinBounds(position, width, height):
	return position.x < width && position.x >= 0 && position.y >= 0 && position.y < height; 
static func fillLine(grid, pos1, pos2):
		if(pos1.x != pos2.x):
			for i in range(min(pos1.x, pos2.x), max(pos1.x, pos2.x)+1):
				grid[pos1.y][i] = 1;
		else:
			for i in range(min(pos1.y, pos2.y), max(pos1.y, pos2.y)+1):
				grid[i][pos1.x] = 1;
				
static func createEmptyGrid(height, width):
	var a = []

	for y in range(height):
		a.append([])
		a[y].resize(width)
		
		for x in range(width):
				a[y][x] = 0	

	return a

static func createEmptyNullGrid(height, width):
	var a = []

	for y in range(height):
		a.append([])
		a[y].resize(width)
		
		for x in range(width):
				a[y][x] = null

	return a
	
static func createMultiCheckpointMaze(height, width, directionBias, randomness, minLengthRun, maxLengthRun, extraTargetPoints):
	var grid = createEmptyGrid(height, width)
	var numberOfPoints = extraTargetPoints+2;
	var allPoints = []
	allPoints.append(Vector2(0, 0))
	for p in range(0, extraTargetPoints):
		allPoints.append(Vector2(randomInt(0, width-1), randomInt(0, height-1)))
	allPoints.append(Vector2(width-1, height-1))
	var i = 0;
	var currentPoint = 1;

	var currentPosition : Vector2 = Vector2(allPoints[0].x, allPoints[0].y);
	var movement : Vector2;
	var nextPosition : Vector2;

	while !isCloseTo(currentPosition,allPoints[numberOfPoints-1], 0):
		i+=1;
		if(i == 50000):
			print("Max loop count hit")
			return;
		
		if( currentPoint != numberOfPoints-1 && isCloseTo(currentPosition, allPoints[currentPoint], 2)):
			currentPoint += 1;
			if(currentPoint == numberOfPoints):
				return {'grid':grid, 'startPosition':allPoints[0]}

				
		var towardsMultiplier = getUnitDifference(allPoints[currentPoint], currentPosition)
		
		var xBias = directionBias/width * abs(currentPosition.x - allPoints[currentPoint].x) - abs(currentPosition.y - allPoints[currentPoint].y);
		
		movement = randomMovement(randomInt(minLengthRun, maxLengthRun), towardsMultiplier, xBias, randomness);
		nextPosition = currentPosition + movement;
		if(isWithinBounds(nextPosition, width, height)):
			fillLine(grid, currentPosition, nextPosition);
			currentPosition = nextPosition;			
	
	print(grid)
	print(allPoints[0])
	return {'grid':grid, 'startPosition':allPoints[0]}
