function TestDataTablesAdd(table, stocks) {
    $(document).ready(function () {
        $(table).DataTable();
    });
}
function TestDataTablesRemove(table) {
    $(document).ready(function () {
        $(table).DataTable().destroy();
        // Removes the datatable wrapper from the dom.
        var elem = document.querySelector(table + '_wrapper');
        elem.parentNode.removeChild(elem);
    });
}