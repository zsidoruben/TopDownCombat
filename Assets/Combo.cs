using System;
using System.Collections.Generic;
using System.IO;

public class Combo
{
	public string Name;
	public float score;
	public List<InputAttack> ComboBricks;
	public Combo(List<InputAttack> list, float _score)
	{
		ComboBricks = list;
		score = _score;
	}
}
