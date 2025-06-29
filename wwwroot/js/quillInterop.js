// Quill Rich Text Editor JavaScript Interop

window.QuillInterop = {
    editors: {},

    initialize: function (editorId, placeholder) {
        if (this.editors[editorId]) {
            this.dispose(editorId);
        }

        const container = document.getElementById(editorId);
        if (!container) {
            console.error('Quill container not found:', editorId);
            return false;
        }

        const quill = new Quill(container, {
            theme: 'snow',
            placeholder: placeholder || 'Enter your content...',
            modules: {
                toolbar: [
                    [{ 'header': [1, 2, 3, false] }],
                    ['bold', 'italic', 'underline', 'strike'],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    ['link'],
                    ['clean']
                ]
            }
        });

        this.editors[editorId] = quill;
        return true;
    },

    getHtml: function (editorId) {
        const quill = this.editors[editorId];
        if (quill) {
            return quill.root.innerHTML;
        }
        return '';
    },

    setHtml: function (editorId, html) {
        const quill = this.editors[editorId];
        if (quill) {
            quill.root.innerHTML = html || '';
        }
    },

    getText: function (editorId) {
        const quill = this.editors[editorId];
        if (quill) {
            return quill.getText();
        }
        return '';
    },

    focus: function (editorId) {
        const quill = this.editors[editorId];
        if (quill) {
            quill.focus();
        }
    },

    dispose: function (editorId) {
        if (this.editors[editorId]) {
            delete this.editors[editorId];
        }
    },

    // Setup change event callback
    setupChangeCallback: function (editorId, dotNetRef, methodName) {
        const quill = this.editors[editorId];
        if (quill && dotNetRef) {
            quill.on('text-change', function() {
                const html = quill.root.innerHTML;
                dotNetRef.invokeMethodAsync(methodName, html);
            });
        }
    }
};
