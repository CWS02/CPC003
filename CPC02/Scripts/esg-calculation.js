/* global Vue */
(function () {
    'use strict';
    var config = window.esgCalculationConfig;
    var search = config.search;
    var trafficRows = config.trafficRows || [];
    var trafficCoefficients = {};
    trafficRows.forEach(function (row, index) {
        trafficCoefficients[index] = row.ParameterValue == null ? '' : row.ParameterValue;
    });
    new Vue({
        el: '#app',
        data: {
            category: search.category || 'coldcoal',
            factory: search.factory || '',
            methods: search.methods || '',
            year: search.year,
            trafficRows: trafficRows,
            trafficCoefficients: trafficCoefficients,
            coefficientOptions: config.coefficientOptions || [],
            allParameters: config.parameters || [],
            selectedCoefficient: '',
            searchQuery: ''
        },
        mounted: function () {
            this.ensureSearchDefaults();
            var self = this;
            var setting = this.allParameters.find(function (p) {
                return p.Category === self.category && String(p.Year) === String(self.year) &&
                    (['waste', 'traffic'].indexOf(self.category) < 0 || p.Category2 === self.methods);
            });
            if (setting) {
                var option = this.coefficientOptions.find(function (p) {
                    return String(p.Id).toLowerCase() === String(setting.Id).toLowerCase();
                });
                this.selectedCoefficient = option ? option.Id : '';
            }
        },
        watch: {
            category: function () {
                this.factory = '全部';
                this.methods = '全部';
                this.ensureSearchDefaults();
            }
        },
        computed: {
            factoryOptions: function () {
                if (this.category === 'electricity') return ['全部', '南科廠', '樹谷廠'];
                if (this.category === 'water') return ['全部', '南科廠', '樹谷一期', '樹谷二期'];
                if (this.category === 'fireextin') return ['全部', '南科', '樹谷'];
                if (this.category === 'coldcoal') return ['全部', '南科', '樹谷', '非公司'];
                return [];
            },
            methodOptions: function () {
                if (this.category === 'waste') return ['全部', '掩埋', '焚化處理'];
                if (this.category === 'wastetransportation') return ['全部', '掩埋', '焚化處理'];
                if (this.category === 'traffic') return ['全部', '高鐵', '飛機', '計程車', '捷運', '台灣火車', '地鐵', '私車公用'];
                return [];
            },
            filteredOptions: function () {
                var self = this;
                var query = this.searchQuery.trim().toLowerCase();
                return this.coefficientOptions.filter(function (option) {
                    // Retain the selected option when the display search changes.
                    return option.Id === self.selectedCoefficient ||
                        String(option.Label || '').toLowerCase().indexOf(query) >= 0;
                });
            },
            selectedCoefficientValue: function () {
                var self = this;
                var option = this.coefficientOptions.find(function (p) { return p.Id === self.selectedCoefficient; });
                if (!option || option.Value === null || String(option.Value).trim() === '') return null;
                var value = Number(option.Value);
                return Number.isFinite(value) ? value : null;
            }
        },
        methods: {
            trafficEmission: function (index, divisor) {
                var factor = this.trafficCoefficients[index];
                if (factor == null || String(factor).trim() === '' || !Number.isFinite(Number(factor))) return '';
                return (Number(factor) * Number(this.trafficRows[index].TotalEmission) / divisor).toFixed(4);
            },
            trafficTotalEmission: function (divisor) {
                var total = 0;
                for (var i = 0; i < this.trafficRows.length; i++) {
                    if (this.trafficEmission(i, 1) === '') return '資料不完整';
                    total += Number(this.trafficCoefficients[i]) * Number(this.trafficRows[i].TotalEmission);
                }
                return (total / divisor).toFixed(4);
            },
            ensureSearchDefaults: function () {
                if (this.factoryOptions.length && this.factoryOptions.indexOf(this.factory) < 0)
                    this.factory = this.factoryOptions[0];
                if (this.methodOptions.length && this.methodOptions.indexOf(this.methods) < 0)
                    this.methods = this.methodOptions[0];
            },
            calculateEmission: function (activity, divisor) {
                if (this.selectedCoefficientValue === null || activity === null || activity === undefined || activity === '') return '';
                var amount = Number(activity);
                if (!Number.isFinite(amount)) return '';
                var result = this.selectedCoefficientValue * amount / divisor;
                return Number.isFinite(result) ? result.toFixed(4) : '';
            },
            calculateCarbonEmission: function (activity) { return this.calculateEmission(activity, 1); },
            calculateCarbonEmission2: function (activity) { return this.calculateEmission(activity, 1000); },
            calculateCarbonEmission5: function (activity) { return this.calculateEmission(activity, 1000000); }
        }
    });
}());
