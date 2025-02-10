function triggerPipeline() {
    var branch = document.getElementById("branch").value;
 
    fetch("/Pipeline/TriggerPipeline", {
        method: "POST",
        headers: { "Content-Type": "application/x-www-form-urlencoded" },
        body: "branch=" + encodeURIComponent(branch)
    })
    .then(response => response.json())
    .then(data => {
        document.getElementById("result").innerText = data.message;
    })
    .catch(error => console.error("Error:", error));
}
