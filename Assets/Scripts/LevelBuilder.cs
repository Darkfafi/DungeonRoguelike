using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour {

	private GridGenerator _generator; 
	private DungeonBuilder _dungeonBuilder = new DungeonBuilder();
	private DungeonPainter _dungeonPainter;
	private DungeonFiller _filler;

	// Use this for initialization
	void Awake () {

		_generator = gameObject.AddComponent<GridGenerator> ();
		_generator.CreateGrid (100, 100);
		_filler = new DungeonFiller (_dungeonBuilder,_generator.gridHolder);
		_dungeonPainter = new DungeonPainter (_dungeonBuilder,_generator.gridHolder);
	}

	// Update is called once per frame
	void Start () {
		_dungeonPainter.PaintGrid ();
		_dungeonBuilder.GenerateDungeon (_generator.gridHolder,5,false,70,100);
		_dungeonPainter.PaintDungeon ();
		_filler.SetAllDungeonRoom (40, 0);
	}
}
