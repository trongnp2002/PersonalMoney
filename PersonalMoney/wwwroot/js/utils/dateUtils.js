

export const formatDateUSText = (data) => {
    var date = new Date(data);

    var options = {
        day: 'numeric',
        month: 'long',
        year: 'numeric'
    };

    return date.toLocaleDateString('en-US', options);
  
}