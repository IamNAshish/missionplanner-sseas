/* 30june2026_step2 this file is created for this step2 */

async function loadData()
{
    const response = await fetch("/api/test");

    const data = await response.json();

    alert(data.message);
}

document
    .getElementById("btnTest")
    .addEventListener("click", loadData);