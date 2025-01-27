using Microsoft.AspNetCore.Mvc;

public ActionResult Admin()
{
    var insurees = db.Insurees.ToList();
    return View(insurees);
}

