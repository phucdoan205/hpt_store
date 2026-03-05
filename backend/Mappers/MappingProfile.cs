using AutoMapper;
using backend.DTOs.Products;
using backend.Models.Products;
using backend.DTOs.Catalog.Product;
using backend.DTOs.Catalog.Category;
using backend.DTOs.Shopping.Order;
using backend.Models.Orders;

using backend.DTOs.Users;
using backend.Models.Users;

using backend.DTOs.Auth;
using backend.DTOs.Admin;

using backend.DTOs.Content;
using backend.Models.Content;

using backend.DTOs.Masters;
using backend.Models.Masters;

using backend.DTOs.Promotions;
using backend.Models.Promotions;

namespace backend.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<CreateProductRequestDto, Product>();
            CreateMap<UpdateProductRequestDto, Product>();

            CreateMap<ProductVariant, ProductVariantDto>();
            CreateMap<ProductImage, ProductImageDto>();

            CreateMap<Category, CategoryResponseDto>();
            CreateMap<CreateCategoryRequestDto, Category>();

            CreateMap<Order, OrderResponseDto>();
            CreateMap<CreateOrderRequestDto, Order>();

            CreateMap<OrderDetail, OrderDetailDto>();

            CreateMap<User, UserResponseDto>();
            CreateMap<User, UserProfileDto>();
            CreateMap<CreateUserRequestDto, User>();
            CreateMap<UpdateUserRequestDto, User>();
            CreateMap<CreateStaffRequestDto, User>();

            // Content mappings
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<CreateArticleDto, Article>();
            CreateMap<UpdateArticleDto, Article>();

            CreateMap<ArticleCategory, ArticleCategoryDto>();
            CreateMap<CreateArticleCategoryDto, ArticleCategory>();
            CreateMap<UpdateArticleCategoryDto, ArticleCategory>();

            // Masters mappings
            CreateMap<MasterColor, MasterColorDto>();
            CreateMap<CreateMasterColorDto, MasterColor>();
            CreateMap<UpdateMasterColorDto, MasterColor>();

            CreateMap<MasterSize, MasterSizeDto>();
            CreateMap<CreateMasterSizeDto, MasterSize>();
            CreateMap<UpdateMasterSizeDto, MasterSize>();

            // Promotions mappings
            CreateMap<Coupon, CouponDto>();
            CreateMap<CreateCouponDto, Coupon>();
            CreateMap<UpdateCouponDto, Coupon>();

            CreateMap<UserCoupon, UserCouponDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.CouponName, opt => opt.MapFrom(src => src.Coupon.Name));
            CreateMap<CreateUserCouponDto, UserCoupon>();
            CreateMap<UpdateUserCouponDto, UserCoupon>();
        }
    }
}