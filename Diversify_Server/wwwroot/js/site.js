

function DataTables() {
    $('#stockTable').load(function () {
        $('#stockTable').DataTable();
        alert("Hello World");
    });
}
