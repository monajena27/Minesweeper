using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Minesweeper
{
    public class GridSeparator
    {
        private readonly IIoHandler _fileHandler;

        private readonly List<string[,]> _listOfGrids = new List<string[,]>();

        public GridSeparator(IIoHandler fileHandler)
        {
            _fileHandler = fileHandler ?? throw new ArgumentException(nameof(fileHandler));

        }

        public void Run()
        {
            var fileContent = _fileHandler.ReadFile(new FileStream());
            var gridLines = new List<string>();
            var grid = new Grid();
            foreach(var line in fileContent)
            {
                if ( line == "\n")
                {
                    _listOfGrids.Add(grid.Build(gridLines));
                    gridLines.Clear();
                    continue;
                }

                gridLines.Add(line);
            }
            _listOfGrids.Add(grid.Build(gridLines));
        }


        private string GridToString(string[,] gridArray)
        {
            string matrixGrid = "";
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    matrixGrid += gridArray[x, y];
                }

                matrixGrid += "\n";
            }
            
            return matrixGrid;
        }
        
        public string PrintGrid()
        {
            var listOfGridStrings = _listOfGrids.Select(GridToString);
            return string.Join("",listOfGridStrings);
        }
    }
}