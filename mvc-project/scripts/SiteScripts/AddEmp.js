function onEmpAdd(employee) {
    debugger;
    $.ajax({
        url: `employee/add`,
        type: "POST",
        success: function (res)
        {
            console.log(res)
        },
        error: function (err)
        {
            console.error(err)
        }
    })

}

