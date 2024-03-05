
export const fetchOnGet = async (url) => {
    try {
        const response = await fetch(url, {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            }
        });

        if (!response.ok) {
            throw new Error('Request failed');
        }

        const data = await response.json();
        return data;
    } catch (error) {
        throw error;
    }
}



export const fetchOnPost = async (url,body) => {
    try {
        const response = await fetch(url, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
                RequestVerificationToken: document.getElementsByName("__RequestVerificationToken")[0].value,
            },
            body: JSON.stringify(body)
        });

        // if (!response.ok) {
        //     throw new Error('Request failed');
        // }

        const data = await response.json();
        return data;
    } catch (error) {
        throw error;
    }
}