using Microsoft.AspNetCore.Mvc;

public ActionResult Create(Insuree insuree)
{
    // Base monthly total
    decimal quote = 50m;

    // Calculate based on age
    var age = DateTime.Now.Year - insuree.DateOfBirth.Year;
    if (insuree.DateOfBirth > DateTime.Now.AddYears(-age)) age--;

    if (age <= 18)
    {
        quote += 100;
    }
    else if (age >= 19 && age <= 25)
    {
        quote += 50;
    }
    else if (age > 25)
    {
        quote += 25;
    }

    // Calculate based on car year
    if (insuree.CarYear < 2000)
    {
        quote += 25;
    }
    else if (insuree.CarYear > 2015)
    {
        quote += 25;
    }

    // Calculate based on car make and model
    if (insuree.CarMake == "Porsche")
    {
        quote += 25;
        if (insuree.CarModel == "911 Carrera")
        {
            quote += 25; // Additional $25 for Porsche 911 Carrera
        }
    }

    // Add $10 for each speeding ticket
    quote += insuree.SpeedingTickets * 10;

    // Add 25% to the total if the user has had a DUI
    if (insuree.DUI)
    {
        quote *= 1.25m;
    }

    // Add 50% to the total if it's full coverage
    if (insuree.CoverageType)
    {
        quote *= 1.50m;
    }

    // Set the calculated quote
    insuree.Quote = quote;

    // Save the insuree object to the database
    if (ModelState.IsValid)
    {
        db.Insurees.Add(insuree);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(insuree);
}
