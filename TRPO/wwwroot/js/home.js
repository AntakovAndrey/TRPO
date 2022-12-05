let arrivalButton = document.querySelector('.Arrival_button')
let departureButton = document.querySelector('.Departure_button')

let arrivalTable = document.querySelector('.arrival_table')
let departureTable = document.querySelector('.departure_table')

window.onload=function()
{
    departureButton.classList.add("control_button_active")
    arrivalTable.style.display = "none"
}

arrivalButton.onclick=function()
{
    if(arrivalButton.classList.contains("control_button_active")===false)
    {
        arrivalTable.style.display = ""
        departureTable.style.display = "none"
        departureButton.classList.remove("control_button_active")
        arrivalButton.classList.add("control_button_active")
    }
}

departureButton.onclick=function()
{
    if(departureButton.classList.contains("control_button_active")===false)
    {
        arrivalTable.style.display = "none"
        departureTable.style.display = ""
        arrivalButton.classList.remove("control_button_active")
        departureButton.classList.add("control_button_active")
    }
    
}