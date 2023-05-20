const bookmarkRoot = 'api/bookmarks';
const tagRoot = 'api/tags';

export default {
    loadAll: () => `${bookmarkRoot}`,
    //   removeArticleFromOrderList: (articleId: string) => `${root}/${articleId}`,
    addBookmark: () => `${bookmarkRoot}`,
    deleteBookmark: (name: string) => `${bookmarkRoot}/${name}`,
    loadAllTags: () => `${tagRoot}`,
    //   changeArticleNumber: (articleId: string) => `${root}/${articleId}/articleNumber`,
    //   changeArticleDescription: (articleId: string) => `${root}/${articleId}/description`,
    //   changeArticleManufacturer: (articleId: string) => `${root}/${articleId}/manufacturer`,
    //   changeArticleQuantity: (articleId: string) => `${root}/${articleId}/quantity`,
};