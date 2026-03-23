document.addEventListener("DOMContentLoaded", function () {
    const modalTitle = document.getElementById("companyModalTitle");
    const form = document.getElementById("companyForm");

    // Edit button
    document.querySelectorAll(".edit-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            modalTitle.textContent = "Edit Company";
            document.getElementById("companyId").value = this.dataset.id;
            document.getElementById("companyName").value = this.dataset.name;
            document.getElementById("location").value = this.dataset.location;
            document.getElementById("phone").value = this.dataset.phone;
            document.getElementById("remark").value = this.dataset.remark;
        });
    });

    // Reset modal on close
    const modal = document.getElementById("companyModal");
    modal.addEventListener("hidden.bs.modal", () => {
        modalTitle.textContent = "Add Company";
        form.reset();
        document.getElementById("companyId").value = "";
    });

    // Delete button
    document.querySelectorAll(".delete-btn").forEach(btn => {
        btn.addEventListener("click", function () {
            if (!confirm("Are you sure you want to delete this company?")) return;
            const id = this.dataset.id;
            fetch("/Company/Delete", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ id })
            })
                .then(res => res.json())
                .then(data => {
                    alert(data.message);
                    location.reload();
                });
        });
    });
});