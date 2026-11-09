const assert = require('node:assert/strict');
const fs = require('node:fs');
const vm = require('node:vm');
let options;
vm.runInNewContext(fs.readFileSync('CPC02/Scripts/esg-calculation.js', 'utf8'), {
    window: { esgCalculationConfig: {
        search: { category: 'electricity', year: '2026' },
        coefficientOptions: [
            { Id: 'ABC', Label: 'Electricity', Value: '0.5' },
            { Id: 'ZERO', Label: 'Zero', Value: '0' },
            { Id: 'INVALID', Label: 'Invalid', Value: '1oops' }
        ],
        parameters: [{ Id: 'abc', Category: 'electricity', Year: 2026 }]
    } },
    Vue: function (definition) { options = definition; }
});
const instance = { ...options.data };
for (const [name, method] of Object.entries(options.methods)) instance[name] = method.bind(instance);
for (const [name, getter] of Object.entries(options.computed)) Object.defineProperty(instance, name, { get: getter.bind(instance) });
options.mounted.call(instance);
assert.equal(instance.selectedCoefficient, 'ABC', 'GUID comparisons ignore case');
assert.equal(instance.calculateCarbonEmission(100), '50.0000');
assert.equal(instance.calculateCarbonEmission2(100), '0.0500');
assert.equal(instance.calculateCarbonEmission5(100), '0.0001');
assert.equal(instance.calculateCarbonEmission(0), '0.0000', 'zero activity is a valid result');
instance.searchQuery = 'nothing matches';
assert.equal(instance.filteredOptions.length, 1, 'search retains selected coefficient');
assert.equal(instance.selectedCoefficientValue, 0.5, 'search cannot change numeric coefficient');
instance.selectedCoefficient = 'ZERO';
assert.equal(instance.calculateCarbonEmission(100), '0.0000', 'zero coefficient is valid');
instance.selectedCoefficient = 'INVALID';
assert.equal(instance.calculateCarbonEmission(100), '', 'reject partially numeric coefficient');
instance.selectedCoefficient = 'ABC';
for (const input of [null, undefined, '', 'bad', Infinity]) assert.equal(instance.calculateCarbonEmission(input), '');
instance.selectedCoefficient = '';
assert.equal(instance.calculateCarbonEmission(100), '', 'missing coefficient is not zero');
for (const category of ['electricity', 'water', 'fireextin', 'coldcoal', 'traffic', 'waste', 'wastetransportation']) {
    instance.category = category;
    instance.factory = '';
    instance.methods = '';
    options.watch.category.call(instance);
    if (instance.factoryOptions.length) assert.equal(instance.factory, instance.factoryOptions[0]);
    if (instance.methodOptions.length) assert.equal(instance.methods, instance.methodOptions[0]);
}
instance.category = 'water';
instance.factory = '樹谷二期';
options.watch.category.call(instance);
assert.equal(instance.factory, '全部', 'switching category defaults to all factories');
instance.trafficRows = [{ TotalEmission: 100 }, { TotalEmission: 50 }];
instance.trafficCoefficients = { 0: '0.1', 1: '0.2' };
assert.equal(instance.trafficTotalEmission(1), '20.0000', 'sum each transport mode with its own factor');
assert.equal(instance.trafficTotalEmission(1000), '0.0200');
instance.trafficCoefficients[1] = '';
assert.equal(instance.trafficTotalEmission(1000), '資料不完整', 'missing transport factor must not understate totals');
console.log('ESG frontend regression checks passed.');
