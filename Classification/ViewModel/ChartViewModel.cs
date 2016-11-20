using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using Classification.Algorithm;
using Classification.Model;
using Classification.Util;
using Syncfusion.Windows.Chart;
using Syncfusion.Windows.Shared;

namespace Classification.ViewModel
{
    public class ChartViewModel : NotificationObject
    {
        private readonly BuildingContext _db;

        public ChartViewModel()
        {
            _db = new BuildingContext();
            _db.Buildings.Load();

            NormalizeCommand = new CommandHandler(Normalize, true);
            DrawGraphCommand = new CommandHandler(DrawGraph, true);

            AddBuildingCommand = new CommandHandler(AddBuilding, true);
            EditBuildingCommand = new CommandHandler(EditBuilding, true);
            DeleteBuildingCommand = new CommandHandler(DeleteBuilding, true);

            Buildings = new ItemChangedObservableCollection<Building>(_db.Buildings.Local);
            Vertices = new ObservableCollection<Vertex>(Buildings.ToListVertices());

            ChartTypes = ChartTypes.Scatter;

            SelectedItem = _db.Buildings.FirstOrDefault();
        }

        private ItemChangedObservableCollection<Building> _buildings;
        public ItemChangedObservableCollection<Building> Buildings
        {
            get
            {
                return _buildings;
            }
            set
            {
                if (_buildings == value) return;
                _buildings = value;
                RaisePropertyChanged("Buildings");
            }
        }

        private ObservableCollection<Vertex> _vertices;
        public ObservableCollection<Vertex> Vertices
        {
            get
            {
                return _vertices;
            }
            set
            {
                if (_vertices == value) return;
                _vertices = value;
                RaisePropertyChanged("Vertices");
            }
        }

        private Building _selectedItem;
        public Building SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem == value) return;
                _selectedItem = value;

                if (value != null)
                {
                    Year = _selectedItem.Year;
                    CountFloors = _selectedItem.CountFloors;
                }

                RaisePropertyChanged("SelectedBuilding");
            }
        }

        private ChartTypes _chartTypes;
        public ChartTypes ChartTypes
        {
            get { return _chartTypes; }
            set
            {
                if (_chartTypes == value) return;
                _chartTypes = value;
                RaisePropertyChanged("ChartTypes");
            }
        }

        private bool _isNormalized;
        public bool IsNormalized
        {
            get { return _isNormalized; }
            set
            {
                _isNormalized = value;
                RaisePropertyChanged("IsNormalized");
            }
        }

        private bool _isTreeBuilt;
        public bool IsTreeBuilt
        {
            get { return _isTreeBuilt; }
            set
            {
                _isTreeBuilt = value;
                RaisePropertyChanged("IsTreeBuilt");
            }
        }

        private int _year;
        private int _countFloors;

        public int Year
        {
            get { return _year; }
            set
            {
                if (_year == value) return;
                _year = value;
                RaisePropertyChanged("Year");
            }
        }

        public int CountFloors
        {
            get { return _countFloors; }
            set
            {
                if (_countFloors == value) return;
                _countFloors = value;
                RaisePropertyChanged("CountFloors");
            }
        }

        public ICommand DrawGraphCommand { get; set; }
        public ICommand NormalizeCommand { get; set; }
        public ICommand AddBuildingCommand { get; set; }
        public ICommand EditBuildingCommand { get; set; }
        public ICommand DeleteBuildingCommand { get; set; }

        public void Normalize()
        {
            Vertices = new ObservableCollection<Vertex>(Vertices.Normalize());

            IsNormalized = true;
        }

        public void AddBuilding()
        {
            var newBuilding = new Building(Year, CountFloors);

            _db.Buildings.Add(newBuilding);
            _db.SaveChanges();

            Buildings.Add(newBuilding);
            Vertices.Add(newBuilding.ToVertex());
            
            ToStartState();
        }

        public void EditBuilding()
        {
            var editingBuilding = _db.Buildings.Find(SelectedItem.Id);
            
            editingBuilding.Year = Year;
            editingBuilding.CountFloors = CountFloors;
            
            _db.Entry(editingBuilding).State = EntityState.Modified;
            _db.SaveChanges();

            var editingVertex = Vertices.First(vertex => vertex.Id == editingBuilding.Id);

            editingVertex.X = CountFloors;
            editingVertex.Y = Year;

            ToStartState();
        }

        public void DeleteBuilding()
        {
            var deletingBuilding = _db.Buildings.Find(SelectedItem.Id);
            
            _db.Buildings.Remove(deletingBuilding);
            _db.SaveChanges();

            Buildings.Remove(deletingBuilding);

            Vertices.Remove(Vertices.First(vertex => vertex.Id == deletingBuilding.Id));

            ToStartState();
        }
        private void ToStartState()
        {
            if (IsNormalized || IsTreeBuilt)
            {
                Vertices = new ObservableCollection<Vertex>(Buildings.ToListVertices());
                IsNormalized = false;
                IsTreeBuilt = false;
                ChartTypes = ChartTypes.Scatter;
            }
        }
        
        public void DrawGraph()
        {
            ChartTypes = ChartTypes.Line;
            IsTreeBuilt = true;

            Vertices = new ObservableCollection<Vertex>(GetQueneVertices());
        }

        private IList<Vertex> GetQueneVertices()
        {
            var queneVertices = new List<Vertex>();
            FindNextVertex(Vertices, 0, queneVertices);

            return queneVertices;
        }

        private void FindNextVertex(IList<Vertex> vertices, int index, IList<Vertex> queneVertices)
        {
            var incidenceMatrix = Prima.GetIncidenceMatrix(vertices);
            
            for (int j = 0; j < vertices.Count; j++)
            {
                if (incidenceMatrix[index, j] == 1)
                {
                    incidenceMatrix[index, j] = 0;
                    queneVertices.Add(vertices[index]);
                    FindNextVertex(vertices, j, queneVertices);
                    queneVertices.Add(vertices[j]);
                }
            }
        }
    }
}
