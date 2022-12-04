let arrivalButton = document.querySelector('.Arrival_button')
let departureButton = document.querySelector('.Departure_button')

let arrivalTable = document.querySelector('.arrival_table')
let departureTable = document.querySelector('.departure_table')

window.onload=function()
{
    changeButtonState(departureButton)
    changetTableVisability(arrivalTable)
}

arrivalButton.onclick=function()
{
    if(arrivalButton.classList.contains("control_button_active")===false)
    {
        changetTableVisability(arrivalTable)
        changetTableVisability(departureTable)
        departureButton.classList.remove("control_button_active")
        arrivalButton.classList.add("control_button_active")
    }
}

departureButton.onclick=function()
{
    if(departureButton.classList.contains("control_button_active")===false)
    {
        changetTableVisability(arrivalTable)
        changetTableVisability(departureTable)
        arrivalButton.classList.remove("control_button_active")
        departureButton.classList.add("control_button_active")
    }
    
}


function changetTableVisability(table)
{
    if(table.style.display === "none")
        {
            table.style.display = ""
        }
    else
        {
            table.style.display = "none"
        }
}