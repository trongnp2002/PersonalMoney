


export const pieChart = (id,height,data) => {
    try {
        var ctx = document.getElementById(id);
        ctx.height = height;
        if (ctx) {
            var myChart = new Chart(ctx, {
                type: 'pie',
                data: data,
                options: {
                    legend: {
                        position: 'top',
                        labels: {
                            fontFamily: 'Poppins'
                        }

                    },
                    responsive: true
                }
            });
        }
    } catch (error) {
        console.log(error);
    }
}


export const polarChart = (id, height, data) => {
    try {
        var ctx = document.getElementById(id);
        ctx.height = height;
        if (ctx) {
            var myChart = new Chart(ctx, {
                type: 'polarArea',
                data: data,
                options: {
                    legend: {
                        position: 'top',
                        labels: {
                            fontFamily: 'Poppins'
                        }

                    },
                    responsive: true
                }
            });
        }

    } catch (error) {
        console.log(error);
    }
}

export const doughutChart = (id, height, data) => {
    try {
        var ctx = document.getElementById(id);
        if (ctx) {
            ctx.height = height;
            var myChart = new Chart(ctx, {
                type: 'doughnut',
                data: data,
                options: {
                    legend: {
                        display: false,
                        position: 'top',
                        labels: {
                            fontFamily: 'Poppins'
                        }

                    },
                    responsive: true
                }
            });
        }
    } catch (error) {
        console.log(error);
    }
}