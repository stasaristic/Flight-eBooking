"use strict"

var connection = new signalR.HubConnectionBuilder().withUrl("/reservationsHub").build();


connection.on("UpdateReservationStatus", function (status, id) {
    var statusResCell = document.querySelector(`tr[data-id="${id}"] #statusRes`);
    statusResCell.innerHTML = status;
    if (status === "Approved")
    {
        var currentBtn = document.querySelector(`tr[data-id="${id}"] #approveButton`);
        if (currentBtn != null)
        {
            currentBtn.value = "Decline";
            currentBtn.className = "btn btn-danger text-white";
            currentBtn.setAttribute = "declineButton";
        }       
    }
    else if (status === "Declined") {
        var currentBtn = document.querySelector(`tr[data-id="${id}"] #declineButton`);
        if (currentBtn != null)
        {
            currentBtn.value = "Approve";
            currentBtn.className = "btn btn-success text-white";
            currentBtn.setAttribute = "approveButton";
        }        
    }
});

connection.start();

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

