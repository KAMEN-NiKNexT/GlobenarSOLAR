using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamen.UI
{
    public class Calendar : SingletonComponent<Calendar>
    {
        #region Enums

        private enum DateType
        {
            Year,
            Month,
            Day
        }

        #endregion

        #region Classes

        [Serializable] private class DateScroll
        {
            #region DateScroll Variables

            [Header("Main Settings")]
            [SerializeField] private DateType _type;
            [SerializeField] private InfiniteScroll _scroll;
            [SerializeField] private List<CalendarPanel> _panels = new List<CalendarPanel>();
            [SerializeField] private int _minValue;
            [SerializeField] private int _maxValue;

            [Header("Additional Settings")]                       
            [SerializeField] private bool _isValueNotChanging;

            [Header("Variables")]
            private bool _isInitialized;

            #endregion

            #region DateScroll Properties

            public DateType Type { get => _type; }
            public InfiniteScroll Scroll { get => _scroll; }
            public List<CalendarPanel> Panels { get => _panels; }
            public int MinValue { get => _minValue; }
            public int MaxValue { get => _maxValue; }

            #endregion

            #region Methods
            
            public void Initialize(int maxValue)
            {
                if (_isInitialized) return;

                _maxValue = maxValue;
                _isInitialized = true;
            }
            public void SetNewMinAndMaxValue(int newMin, int newMax)
            {
                if (_isValueNotChanging) return;

                _minValue = newMin;
                _maxValue = newMax;
            }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private DateScroll _yearScroll;
        [SerializeField] private DateScroll _monthScroll;
        [SerializeField] private DateScroll _dayScroll;

        [Header("Settings")]
        [SerializeField] private int _numberChoosenDate;
        [SerializeField] private string[] _monthsNames;

        [Header("Variables")]
        private List<DateTime> _dates = new List<DateTime>();
        public Action<DateTime> OnChoosenDateTimeChanged;

        #endregion

        #region Control Methods

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            SetUpDate();
            _yearScroll.Initialize(_dates[_numberChoosenDate].Year);
            _yearScroll.Scroll.OnPanelMoved += UpdateYearScrollsValue;
            _monthScroll.Scroll.OnPanelMoved += UpdateMonthScrollsValue;
            _dayScroll.Scroll.OnPanelMoved += UpdateDayScrollsValue;
            UpdateDate(DateType.Year, 0);
            UpdateDate(DateType.Month, 0);
            UpdateDate(DateType.Day, 0);
            UpdateAllPanels();
        }
        private void SetUpDate()
        {
            DateTime currentDate = DateTime.Now;
            for (int i = 0; i < _yearScroll.Panels.Count; i++)
            {
                _dates.Add(currentDate);
                _dates[i] = _dates[i].AddYears(i - _numberChoosenDate);
                int year = _dates[i].Year;
                _dates[i] = _dates[i].AddMonths(i - _numberChoosenDate);
                if (year != _dates[i].Year) _dates[i] = _dates[i].AddYears(-_dates[i].Year + year);
                int month = _dates[i].Month;
                _dates[i] = _dates[i].AddDays(i - _numberChoosenDate);
                if (year != _dates[i].Year) _dates[i] = _dates[i].AddYears(-_dates[i].Year + year);
                if (month != _dates[i].Month) _dates[i] = _dates[i].AddMonths(-_dates[i].Month + month);
            }
            Debug.Log(_dates.Count);
        }
        private void UpdateDate(DateType type, int value)
        {
            switch(type)
            {
                case DateType.Year:
                    for (int i = 0; i < _dates.Count; i++)
                    {
                        _dates[i] = _dates[i].AddYears(value);
                        if (_dates[i].Year > _yearScroll.MaxValue) 
                        {
                            _dates[i] = _dates[i].AddYears(-_dates[i].Year + _yearScroll.MinValue);
                        } 
                        else if (_dates[i].Year < _yearScroll.MinValue)
                        {
                            _dates[i] = _dates[i].AddYears(-_dates[i].Year + _yearScroll.MaxValue);
                        }
                        _dayScroll.SetNewMinAndMaxValue(1, DateTime.DaysInMonth(_dates[i].Year, _dates[i].Month));
                        UpdateAllPanels();
                    }
                    break;
                case DateType.Month:
                    for (int i = 0; i < _dates.Count; i++)
                    {
                        int newValue = _dates[i].Month + value;
                        if (newValue > _monthScroll.MaxValue)
                        {
                            _dates[i] = _dates[i].AddMonths(-_dates[i].Month + _monthScroll.MinValue);
                        }
                        else if (newValue < _monthScroll.MinValue)
                        {
                            _dates[i] = _dates[i].AddMonths(-_dates[i].Month + _monthScroll.MaxValue);
                        }
                        else _dates[i] = _dates[i].AddMonths(-_dates[i].Month + newValue);
                    }
                    for (int i = 0; i < _dates.Count; i++)
                    {
                        _dayScroll.SetNewMinAndMaxValue(1, DateTime.DaysInMonth(_dates[i].Year, _dates[i].Month));
                    }
                    UpdateAllPanels();
                    break;
                case DateType.Day:
                    for (int i = 0; i < _dates.Count; i++)
                    {
                        int newValue = _dates[i].Day + value;
                        if (newValue > _dayScroll.MaxValue)
                        {
                            _dates[i] = _dates[i].AddDays(-_dates[i].Day + _dayScroll.MinValue);
                        }
                        else if (newValue < _dayScroll.MinValue)
                        {
                            _dates[i] = _dates[i].AddDays(-_dates[i].Day + _dayScroll.MaxValue);
                        }
                        else _dates[i] = _dates[i].AddDays(value);
                    }
                    break;
                    
            }
            OnChoosenDateTimeChanged?.Invoke(_dates[_numberChoosenDate]);
        }

        private void UpdateAllPanels()
        {
            for(int i = 0; i < _dates.Count; i++)
            {
                UpdateYearPanel(i);
                UpdateMonthPanel(i);
                UpdateDayPanel(i);
            }
        }
        private void UpdateYearPanel(int index) => _yearScroll.Panels[index].UpdatePanel(_dates[index].Year + "");
        private void UpdateMonthPanel(int index) => _monthScroll.Panels[index].UpdatePanel(_monthsNames[_dates[index].Month - 1]);
        private void UpdateDayPanel(int index) => _dayScroll.Panels[index].UpdatePanel(_dates[index].Day + "");

        private void UpdateYearScrollsValue(int oldIndex, int newIndex)
        {
            UpdatePanels(oldIndex, newIndex, _yearScroll);
            UpdateYearPanel(newIndex);
        }
        private void UpdateMonthScrollsValue(int oldIndex, int newIndex)
        {
            UpdatePanels(oldIndex, newIndex, _monthScroll);
            UpdateMonthPanel(newIndex);
        }
        private void UpdateDayScrollsValue(int oldIndex, int newIndex)
        {
            UpdatePanels(oldIndex, newIndex, _dayScroll);
            UpdateDayPanel(newIndex);
        }
        private void UpdatePanels(int oldIndex, int newIndex, DateScroll dateScroll)
        {
            CalendarPanel panel = dateScroll.Panels[oldIndex];
            dateScroll.Panels.RemoveAt(oldIndex);
            dateScroll.Panels.Insert(newIndex, panel);
            UpdateDate(dateScroll.Type, newIndex > oldIndex ? 1 : -1);
        }

        #endregion
    }
}