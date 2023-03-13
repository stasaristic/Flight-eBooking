"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/reservationsHub").build();


connection.on("UpdateReservationStatus", function (status, id) {
    var statusResCell = document.querySelector(`tr[data-id="${id}"] #statusRes`);
    statusResCell.innerHTML = status;
    //alert(status);
    //  Switch buttons on status change
    if (status === "Approved")
    {
        var trBtn = document.querySelector(`tr[data-id="${id}"]`);
        trBtn.cells[10].innerHTML = "";
        trBtn.cells[10].innerHTML = "<input type=\"submit\" value=\"Decline\" class=\"btn btn-danger text-white\" id=\"declineButton\" />";       
    }
    else if (status === "Declined") {
        var trBtn = document.querySelector(`tr[data-id="${id}"]`);
        trBtn.cells[10].innerHTML = "";
        trBtn.cells[10].innerHTML = "<input type=\"submit\" value=\"Approve\" class=\"btn btn-success text-white\" id=\"approveButton\" />";  
    }
});

// function for updating Reservation table that is on Index page
connection.on("NewReservationRecieve", function (id, userName, userEmail, flightName, flightDepartureDate, flightClass, totalPrice, numOfSeats, statusRes) {

    var tableIndex = document.getElementById('indexTable').getElementsByTagName('tbody')[0];
    var rowIndex = tableIndex.insertRow();
    rowIndex.style.textAlign = "center";

    var resId = rowIndex.insertCell(0);
    var userNameTd = rowIndex.insertCell(1);
    var userEmailTd = rowIndex.insertCell(2);
    var flightNameTd = rowIndex.insertCell(3);
    var flightDepartureDateTd = rowIndex.insertCell(4);
    var flightClassTd = rowIndex.insertCell(5);
    var totalPriceTd = rowIndex.insertCell(6);
    var numOfSeatsTd = rowIndex.insertCell(7);
    var statusResTd = rowIndex.insertCell(8);
    var flightDetailsTd = rowIndex.insertCell(9);
    var statusChangeBtnsTd = rowIndex.insertCell(10);

    resId.innerHTML = id;
    resId.style.alignItems = "center";
    userNameTd.innerHTML = userName;
    userNameTd.style.alignItems = "center";
    userEmailTd.innerHTML = userEmail;
    userEmailTd.style.alignItems = "center";
    flightNameTd.innerHTML = flightName;
    flightNameTd.style.alignItems = "center";
    flightDepartureDateTd.innerHTML = flightDepartureDate;
    flightDepartureDateTd.style.alignItems = "center";
    flightClassTd.innerHTML = flightClass;
    flightClassTd.style.alignItems = "center";
    totalPriceTd.innerHTML = totalPrice;
    totalPriceTd.style.alignItems = "center";
    numOfSeatsTd.innerHTML = numOfSeats;
    numOfSeatsTd.style.alignItems = "center";
    statusResTd.innerHTML = statusRes;
    statusResTd.style.alignItems = "center";
    flightDetailsTd.innerHTML = "<a class=\"btn btn-outline-info\" asp-controller=\"Flights\" asp-action=\"Details\" asp-route-id=\"@item.Id\"><i class=\"bi bi-eye\"></i> Flight Details </a>";
    flightDetailsTd.style.alignItems = "center";
    statusChangeBtnsTd.innerHTML =
        "<input type=\"submit\" value=\"Approve\" class=\"btn btn-success text-white\" id=\"approveButton\" />" +
        "<hr/>" +
        "<input type=\"submit\" value=\"Decline\" class=\"btn btn-danger text-white\" id=\"declineButton\"/>";
    statusChangeBtnsTd.style.alignItems = "center";
});

// function for updating MyReservations table
connection.on("MyNewReservationRecieve", function (id, flightName, flightDepartureDate, flightClass, totalPrice, numOfSeats, statusRes) {

    var tableMyRes = document.getElementById('myresTable').getElementsByTagName('tbody')[0];
    var rowMyRes = tableMyRes.insertRow();
    rowMyRes.style.textAlign = "center"

    // MR stands for My Reservations so it is easier to differentiate 
    var resIdMR = rowMyRes.insertCell(0);
    var flightNameTdMR = rowMyRes.insertCell(1);
    var flightDepartureDateTdMR = rowMyRes.insertCell(2);
    var flightClassTdMR = rowMyRes.insertCell(3);
    var totalPriceTdMR = rowMyRes.insertCell(4);
    var numOfSeatsTdMR = rowMyRes.insertCell(5);
    var statusResTdMR = rowMyRes.insertCell(6);
    var flightDetailsTdMR = rowMyRes.insertCell(7);

    resIdMR.innerHTML = id;
    resIdMR.style.alignItems = "center";
    flightNameTdMR.innerHTML = flightName;
    flightNameTdMR.style.alignItems = "center";
    flightDepartureDateTdMR.innerHTML = flightDepartureDate;
    flightDepartureDateTdMR.style.alignItems = "center";
    flightClassTdMR.innerHTML = flightClass;
    flightClassTdMR.style.alignItems = "center";
    totalPriceTdMR.innerHTML = totalPrice;
    totalPriceTdMR.style.alignItems = "center";
    numOfSeatsTdMR.innerHTML = numOfSeats;
    numOfSeatsTdMR.style.alignItems = "center";
    statusResTdMR.innerHTML = statusRes;
    statusResTdMR.style.alignItems = "center";
    flightDetailsTdMR.innerHTML = "<a class=\"btn btn-outline-info\" asp-controller=\"Flights\" asp-action=\"Details\" asp-route-id=\"@item.Id\"><i class=\"bi bi-eye\"></i> Flight Details </a>";
    flightDepartureDateTdMR.style.alignItems = "center";
});


connection.start();

/*   SignalR for New Reservation     */

function handleReserveTicketBtnClick(id, userName, userEmail, flightName, flightDepartureDate, flightClass, totalPrice, numOfSeats, statusRes) {
    connection.invoke('NewReservationSend', id, userName, userEmail, flightName,
        flightDepartureDate, flightClass, totalPrice, numOfSeats, statusRes)
        .catch(function (err) {
            return console.error(err.toString());
        });
    connection.invoke('MyNewReservationSend', id, flightName,
        flightDepartureDate, flightClass, totalPrice, numOfSeats, statusRes)
        .catch(function (err) {
            return console.error(err.toString());
        });
}

var reserveTicketBtn = document.querySelectorAll('#reserveTicketBtn');
reserveTicketBtn.forEach(function (button) {
    button.addEventListener('click', handleReserveTicketBtnClick);
});

/*   SignalR for Update Reservation Status    */
function handleApproveButtonClick(reservationId) {
    connection.invoke('UpdateResStatus', "Approved", reservationId);
}

var approveButtons = document.querySelectorAll('#approveButton');
approveButtons.forEach(function (button) {
    button.addEventListener('click', handleApproveButtonClick);
});

function handleDeclineButtonClick(reservationId) {
    connection.invoke('UpdateResStatus', "Declined", reservationId);
}

var declineButtons = document.querySelectorAll('#declineButton');
declineButtons.forEach(function (button) {
    button.addEventListener('click', handleDeclineButtonClick);
});

