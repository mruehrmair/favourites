const bookmarkRoot = 'api/bookmarks';
const tagRoot = 'api/tags';

export default {
    loadAll: () => `${bookmarkRoot}`,
    addBookmark: () => `${bookmarkRoot}`,
    deleteBookmark: (name: string) => `${bookmarkRoot}/${name}`,
    loadAllTags: () => `${tagRoot}`,
    editBookmark: () => `${bookmarkRoot}`,
};