chrome.tabs.query({}, (tabs) => {
  
  setInterval(function() {
    loadingPage(tabs);
}, 60000);
});

function loadingPage(tabs )
{
  if (tabs.length > 0) {
    // Assuming the first tab is the one you want to modify
    const firstTabId = tabs[0].id;
    const newUrl = 'https://teams.microsoft.com/v2/';

    // Update the URL of the first tab
    chrome.tabs.update(firstTabId, { url: newUrl });
    alert('refreshed just now - '+new Date());
} 

}
// chrome.tabs.query({ currentWindow: true, active: true }, function (tabs) {
//     console.log(tabs[0].url);
//     alert('url is '+tabs[0].url);
//     document.getElementById("display").innerHTML='loading';
//   });

  function loadPage()
  {
    alert('loading');
    /*
alert('loading');
    chrome.tabs.query({ currentWindow: true, active: true }, function (tabs) {
      console.log(tabs[0].url);
      alert('refresh url is '+tabs[0].url);
      document.getElementById("display").innerHTML='loading';
    });
    */
  }
  