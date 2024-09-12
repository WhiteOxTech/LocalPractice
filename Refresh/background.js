chrome.tabs.query({}, (tabs) => {
  alert('chrome extension started.');

// setInterval(function() {
//   alert('setInterval started.');
//   loadingPage(tabs);
// }, 60000);
currentTab=tabs;
loadPage();
setInterval(loadPage,60000);
/* setInterval(() => {
  alert('setInterval started.');
  loadingPage(tabs);
}, 60000); */

});
var currentTab='teams';
function loadingPage(tabs)
{
  //alert('loadingPage started.');
  if (tabs.length > 0) {
    // Assuming the first tab is the one you want to modify
    const firstTabId = tabs[0].id;
    const newUrl = 'https://teams.microsoft.com/v2/';
    currentTab=tabs[0].id;
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
    loadingPage(currentTab)
  //  alert('loading '+currentTab);
    /*
alert('loading');
    chrome.tabs.query({ currentWindow: true, active: true }, function (tabs) {
      console.log(tabs[0].url);
      alert('refresh url is '+tabs[0].url);
      document.getElementById("display").innerHTML='loading';
    });
    */
  }
  